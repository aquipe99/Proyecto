using Microsoft.AspNetCore.Mvc;
using SR.ServiceClient.SCMenu;
using System.Security.Claims;

namespace mvc.Presentation.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuClient _menuClient;

        public MenuController(IMenuClient menuClient)
        {
            _menuClient = menuClient;
        }

        public IActionResult SidebarMenu()
        {
            var menu = _menuClient.ObtenerMenuPorusuario(int.Parse(ClaimTypes.NameIdentifier));
            return PartialView("_SidebarMenu",menu);
        }
        public IActionResult UsuarioInfo() {

            return PartialView("_UserInfo");
        }
    }
}
