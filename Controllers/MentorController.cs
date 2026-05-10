using Microsoft.AspNetCore.Mvc;
using EduTrip.Models;
using Microsoft.EntityFrameworkCore;

namespace EduTrip.Controllers
{
    public class MentorController : Controller
    {
        private readonly EduTripContext _context;

        public MentorController(EduTripContext context)
        {
            _context = context;
        }

        // GET: /Mentor/Index
        public async Task<IActionResult> Index()
        {
            // Fetch only approved mentors with their basic user information
            var mentors = await _context.MentorProfiles
                .Include(m => m.User)
                .Where(m => m.IsApproved)
                .ToListAsync();

            return View(mentors);
        }

        // GET: /Mentor/Details/{id}
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Load mentor profile, user details, and available time slots
            var mentor = await _context.MentorProfiles
                .Include(m => m.User)
                .Include(m => m.MentorAvailabilities)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mentor == null)
            {
                return NotFound();
            }

            return View(mentor);
        }
    }
}