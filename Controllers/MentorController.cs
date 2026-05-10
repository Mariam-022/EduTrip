using Microsoft.AspNetCore.Mvc;

namespace EduTrip.Controllers
{
    public class MentorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
