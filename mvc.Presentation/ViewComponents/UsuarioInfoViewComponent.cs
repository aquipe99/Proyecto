using Microsoft.AspNetCore.Mvc;
using mvc.Entities.BaseEntities.UsuarioEntities;
using System.Security.Claims;

namespace SR.Presentation.ViewComponents
{
    public class UsuarioInfoViewComponent :ViewComponent
    {
        public IViewComponentResult Invoke() {

            var claims = HttpContext.User;

            var nombre = claims.FindFirst(ClaimTypes.Name)?.Value ?? "";
            var rol_name = claims.FindFirst(ClaimTypes.Role)?.Value ?? "";

            var modelo = new Usuario
            {
                Nombre = nombre,
                RolName = rol_name
            };

            return View("UsuarioInfo", modelo);
        }
    }
}
