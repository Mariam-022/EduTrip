using Microsoft.AspNetCore.Mvc;
using EduTrip.Models;
using Microsoft.EntityFrameworkCore;

namespace EduTrip.Controllers
{
    public class PaymentController : Controller
    {
        private readonly EduTripContext _context;

        public PaymentController(EduTripContext context)
        {
            _context = context;
        }

        // ─────────────────────────────────────────────────────────
        // ACTION: FakePayment (GET)
        // ─────────────────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> FakePayment(Guid orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Mentor)
                    .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return RedirectToAction("Index", "Home");

            return View(order);
        }

        // ─────────────────────────────────────────────────────────
        // ACTION: FakePayment (POST)
        // FIX: Different C# method name + [ActionName] so the router
        // still maps POST requests to "FakePayment" on this controller.
        // ─────────────────────────────────────────────────────────
        [HttpPost]
        [ActionName("FakePayment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FakePaymentPost([FromForm(Name = "orderId")] Guid id)
        {
            // 1. Load the order
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return RedirectToAction("Index", "Home");

            // 2. Logic Check: Ensure booking exists BEFORE processing payment logic
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.OrderId == id);

            if (booking == null)
            {
                // If there's no booking, we shouldn't process payment.
                return RedirectToAction("Index", "Home");
            }

            // 3. Create a Payment record
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Provider = "Fake",
                ExternalId = "FAKE-" + Guid.NewGuid().ToString().Substring(0, 8),
                Amount = order.Amount,
                Status = "Success",
                PaidAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Payments.Add(payment);

            // 4. Update Order status
            order.Status = "Paid";
            order.UpdatedAt = DateTime.UtcNow;

            // 5. Create MentorEarnings record
            if (order.MentorId.HasValue)
            {
                var earning = new MentorEarning
                {
                    Id = Guid.NewGuid(),
                    MentorId = order.MentorId.Value,
                    OrderId = order.Id,
                    GrossAmount = order.Amount,
                    CommissionRate = 70.00m,
                    MentorShare = order.Amount * 0.70m,
                    PlatformShare = order.Amount * 0.30m,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.MentorEarnings.Add(earning);
            }

            // 6. Save all changes
            await _context.SaveChangesAsync();

            // 7. Redirect using the booking found in Step 2
            return RedirectToAction("Confirmation", "Booking", new { bookingId = booking.Id });
        }
    }
}