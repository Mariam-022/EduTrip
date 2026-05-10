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

        // ─────────────────────────────────────────────────────────
        // ACTION: Index
        // ROUTE: GET /Mentor/Index
        // WHAT IT DOES: Loads ALL approved mentors from DB and shows them as cards
        // ─────────────────────────────────────────────────────────
        public async Task<IActionResult> Index()
        {
            // LINQ QUERY — reads like English:
            // "Go to MentorProfiles table,
            //  also load the related User for each mentor (JOIN on UserId),
            //  only keep rows where IsApproved is true (1),
            //  put the results in a List"
            //
            // SQL EQUIVALENT:
            // SELECT mp.*, u.*
            // FROM MentorProfiles mp
            // INNER JOIN Users u ON mp.UserId = u.Id
            // WHERE mp.IsApproved = 1
            var mentors = await _context.MentorProfiles
                .Include(m => m.User)          // JOIN with Users table (gets FirstName, LastName, Email)
                .Where(m => m.IsApproved)      // Filter: only show approved mentors
                .ToListAsync();                // Execute the query, return as List<MentorProfile>

            // Pass the list of mentors to the View.
            // The View will loop over this list and create one card per mentor.
            // return View(mentors) puts 'mentors' into Model variable in the .cshtml file.
            return View(mentors);
        }

        // ─────────────────────────────────────────────────────────
        // ACTION: Details
        // ROUTE: GET /Mentor/Details/{id}
        // Example URL: /Mentor/Details/3fa85f64-5717-4562-b3fc-2c963f66afa6
        // WHAT IT DOES: Load ONE mentor + their availability slots
        // ─────────────────────────────────────────────────────────
        public async Task<IActionResult> Details(Guid? id)
        {
            // 'id' comes FROM the URL automatically.
            // If user visits /Mentor/Details/abc-123, then id = Guid("abc-123")
            // The ? means it's nullable — the user MIGHT visit /Mentor/Details with no id

            // VALIDATION: Did they give us an ID at all?
            if (id == null)
            {
                // If no ID → send them back to the mentor list
                // RedirectToAction("Index") → HTTP 302 redirect to /Mentor/Index
                return RedirectToAction("Index");
            }

            // QUERY: Find the specific mentor by ID
            // .FirstOrDefaultAsync() → Get the first match, or NULL if not found
            // SQL: SELECT * FROM MentorProfiles mp
            //       JOIN Users u ON mp.UserId = u.Id
            //       LEFT JOIN MentorAvailability ma ON ma.MentorId = mp.Id
            //       WHERE mp.Id = @id
            var mentor = await _context.MentorProfiles
                .Include(m => m.User)                    // Get mentor's name, email
                .Include(m => m.MentorAvailabilities)    // Get ALL their time slots
                .FirstOrDefaultAsync(m => m.Id == id);  // Match on the ID from the URL

            // SAFETY CHECK: What if someone types a random/wrong ID?
            if (mentor == null)
            {
                // 404 — Mentor not found. Returns the default 404 page.
                return NotFound();
            }

            // SUCCESS: Pass the single mentor object to the Details view.
            // In Details.cshtml, we access it as @Model (not a list this time — just one mentor)
            return View(mentor);
        }
    }
}