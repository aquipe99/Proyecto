using Microsoft.AspNetCore.Mvc;
using SR.ServiceClient.SCMenu;
using System.Security.Claims;

namespace SR.Presentation.ViewComponents
{
    public class SiderbarViewComponent :ViewComponent
    {
        private readonly IMenuClient _menuClient;

        public SiderbarViewComponent(IMenuClient menuClient)
        {
            _menuClient = menuClient;
        }
        public  Task<IViewComponentResult> InvokeAsync() {

            var claims = HttpContext.User;

            var idClaim = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = int.TryParse(idClaim, out var idParsed) ? idParsed : 0;
            var menu =  _menuClient.ObtenerMenuPorusuario(userId);

            return Task.FromResult<IViewComponentResult>(View("Siderbar", menu));
        }
    }
}
