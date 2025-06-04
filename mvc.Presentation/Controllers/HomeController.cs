using Microsoft.AspNetCore.Mvc;

namespace mvc.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
