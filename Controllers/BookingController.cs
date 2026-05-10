using Microsoft.AspNetCore.Mvc;
using EduTrip.Models;
using Microsoft.EntityFrameworkCore;

namespace EduTrip.Controllers
{
    public class BookingController : Controller
    {
        private readonly EduTripContext _context;

        public BookingController(EduTripContext context)
        {
            _context = context;
        }

        // GET: /Booking/Create
        [HttpGet]
        public async Task<IActionResult> Create(Guid mentorId, Guid availabilityId)
        {
            // Load mentor and availability data to display in the summary
            var mentor = await _context.MentorProfiles
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == mentorId);

            var availability = await _context.MentorAvailabilities
                .FirstOrDefaultAsync(a => a.Id == availabilityId);

            if (mentor == null || availability == null)
                return RedirectToAction("Index", "Mentor");

            ViewBag.Mentor = mentor;
            ViewBag.Availability = availability;

            return View();
        }

        // POST: /Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid mentorId, Guid availabilityId, string studentName, string studentEmail)
        {
            // Check if student exists; if not, create a new record
            var student = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == studentEmail);

            if (student == null)
            {
                var nameParts = studentName.Split(' ');
                student = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = nameParts[0],
                    LastName = nameParts.Length > 1 ? nameParts[1] : "",
                    Email = studentEmail,
                    Role = "Student",
                    AccountTier = "Free",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.Users.Add(student);
            }

            var mentor = await _context.MentorProfiles.FindAsync(mentorId);
            var availability = await _context.MentorAvailabilities.FindAsync(availabilityId);

            if (mentor == null || availability == null)
                return RedirectToAction("Index", "Mentor");

            // Transactional data creation: Order -> Booking -> Update Availability
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = student.Id,
                OrderType = "Session",
                MentorId = mentorId,
                Amount = mentor.HourlyRate,
                Currency = "EGP",
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Orders.Add(order);

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                StudentId = student.Id,
                MentorId = mentorId,
                AvailabilityId = availabilityId,
                ScheduledAt = DateTime.UtcNow.AddDays(7), // Default schedule for demo
                DurationMinutes = 60,
                Status = "Confirmed",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Bookings.Add(booking);

            // Set slot as booked to prevent double-booking
            availability.IsBooked = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("FakePayment", "Payment", new { orderId = order.Id });
        }

        // GET: /Booking/Confirmation/{id}
        public async Task<IActionResult> Confirmation(Guid bookingId)
        {
            var booking = await _context.Bookings
                .Include(b => b.Mentor).ThenInclude(m => m.User)
                .Include(b => b.Student)
                .Include(b => b.Availability)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
                return RedirectToAction(nameof(HomeController.Index), "Home");

            return View(booking);
        }
    }
}