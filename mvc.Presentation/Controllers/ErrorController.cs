using Microsoft.AspNetCore.Mvc;

namespace SR.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error404")]
        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult Error403()
        {
            return View();  // Muestra una vista personalizada con el mensaje "Acceso Denegado"
        }
    }
}
