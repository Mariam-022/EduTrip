using Microsoft.AspNetCore.Mvc;
using EduTrip.Models;
using Microsoft.EntityFrameworkCore;

namespace EduTrip.Controllers
{
    public class DashboardController : Controller
    {
        private readonly EduTripContext _context;

        public DashboardController(EduTripContext context)
        {
            _context = context;
        }

        // ─────────────────────────────────────────────────────────
        // ACTION: Index
        // ROUTE: GET /Dashboard/Index
        // WHAT IT DOES: Shows all bookings for a demo student
        // In production: would filter by the logged-in user's ID
        // ─────────────────────────────────────────────────────────
        public async Task<IActionResult> Index()
        {
            // In production: var userId = GetCurrentUserId();
            // For demo: load ALL bookings (or the most recent ones)
            // This is good enough to show the dashboard flow works.

            // Load bookings with all related data needed for display
            var bookings = await _context.Bookings
                .Include(b => b.Mentor)             // Need mentor info
                    .ThenInclude(m => m.User)       // Need mentor's name
                .Include(b => b.Student)            // Need student info
                .Include(b => b.Availability)       // Need the time slot
                .Include(b => b.Order)              // Need payment amount
                .OrderByDescending(b => b.CreatedAt) // Newest first
                .Take(10)                            // Limit to 10 for demo
                .ToListAsync();

            return View(bookings);
        }
    }
}