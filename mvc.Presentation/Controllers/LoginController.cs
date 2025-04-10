using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using mvc.ServiceClient.SCUsuario;
using System.Security.Claims;

namespace mvc.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioClient _usuarioClient;

        public LoginController(IUsuarioClient usuarioClient)
        {
            _usuarioClient = usuarioClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string gmail,string contrasenia) {

            if (_usuarioClient.ValidarLogin(gmail, contrasenia)) {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,gmail)
                };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync("CookieAuth", principal).Wait();
                return RedirectToAction("Index", "Producto");
            }
            ViewBag.Error = "Credenciales incorrectas.";
            return View("Index");
        }
        public IActionResult Logout() {
            HttpContext.SignOutAsync("CookieAuth").Wait();
            return RedirectToAction("Index","Login");
        
        }
    }
}
