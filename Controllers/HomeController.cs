using Microsoft.AspNetCore.Mvc;
using EduTrip.Models;
using Microsoft.EntityFrameworkCore;

namespace EduTrip.Controllers
{
    public class HomeController : Controller
    {
        private readonly EduTripContext _context;

        // CONSTRUCTOR
        // When .NET creates this controller, it automatically "injects" the database context.
        // This is called Dependency Injection — .NET handles creating _context for us.
        public HomeController(EduTripContext context)
        {
            _context = context;
        }

        // ACTION: Index
        // This runs when user visits: http://yoursite.com/ OR http://yoursite.com/Home/Index
        // It's a GET request — user is just READING data, not submitting anything.
        public async Task<IActionResult> Index()
        {
            // Count how many approved mentors exist in the database
            // _context.MentorProfiles → goes to MentorProfiles table
            // .CountAsync(m => m.IsApproved) → SQL: SELECT COUNT(*) WHERE IsApproved = 1
            var mentorCount = await _context.MentorProfiles
                .CountAsync(m => m.IsApproved);

            // Count total registered students
            // SQL: SELECT COUNT(*) FROM Users WHERE Role = 'Student'
            var studentCount = await _context.Users
                .CountAsync(u => u.Role == "Student");

            // ViewBag is a dynamic bag we use to send small pieces of data to the View.
            // In the View, we access it as: @ViewBag.MentorCount
            ViewBag.MentorCount = mentorCount;
            ViewBag.StudentCount = studentCount;

            // Return the Index view (Views/Home/Index.cshtml)
            return View();
        }
    }
}