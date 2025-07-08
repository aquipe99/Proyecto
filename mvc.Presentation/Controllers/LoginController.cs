using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SR.Entities.BaseEntities.UsuarioEntities;
using SR.Entities.ViewModels;
using SR.ServiceClient.SCUsuario;
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
            var usuario = _usuarioClient.ValidarLogin(model.correo, model.contrasenia);
            if (usuario.LoginResultado == LoginResultado.Exito)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier,usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name,usuario.Nombre ?? string.Empty),
                    new Claim(ClaimTypes.Email,usuario.Correo  ?? string.Empty),
                    new(ClaimTypes.Role,usuario.RolName ?? string.Empty)
                };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync("CookieAuth", principal).Wait();
                return RedirectToAction("Index", "Reserva");
            }
            if (usuario.LoginResultado == LoginResultado.UsuarioBloqueado)
            {
                ModelState.AddModelError(string.Empty, "Su cuenta ha sido desactivada. Contacte al administrador.");
            }
            else {
                ModelState.AddModelError(string.Empty, "Credenciales incorrectas.");
            }             
            return View("Index");
        }
        public IActionResult Logout() {
            HttpContext.SignOutAsync("CookieAuth").Wait();
            return RedirectToAction("Index","Login");        
        }
    }
}
