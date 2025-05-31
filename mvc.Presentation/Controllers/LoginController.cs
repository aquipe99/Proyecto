using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using mvc.Entities.ViewModels;
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
            return View(new LoginViewModel());
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model) {

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            if (_usuarioClient.ValidarLogin(model.correo,model.contrasenia)) {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,model.correo)
                };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync("CookieAuth", principal).Wait();
                return RedirectToAction("Index", "Producto");
            }
            ModelState.AddModelError(string.Empty, "Credenciales incorrectas.");
            return View("Index");
        }
        public IActionResult Logout() {
            HttpContext.SignOutAsync("CookieAuth").Wait();
            return RedirectToAction("Index","Login");
        
        }
    }
}
