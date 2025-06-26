using Microsoft.AspNetCore.Mvc;
using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.BaseEntities.UsuarioEntities;
using SR.Entities.ViewModels;
using SR.ServiceClient.SCMenu;
using SR.ServiceClient.SCUsuario;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace SR.Presentation.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioClient _usuarioClient;
        private readonly IMenuClient _menuClient;

        public UsuarioController(IUsuarioClient usuarioClient,IMenuClient menuClient) { 
            _usuarioClient = usuarioClient;
            _menuClient = menuClient;
            
        }
        public IActionResult Index(int page = 1, int pageSize = 5, string buscar = "")
        {
            var usuarios = _usuarioClient.ObtenerListaUsuario(page, pageSize, buscar);
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalCount"] = (usuarios != null && usuarios.Any()) ? usuarios.First().Total : 0;
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_TablaUsuarios", usuarios);
            }
            return View(usuarios);
        }
        public IActionResult Paginar(int page = 1, int pageSize = 5, string buscar = "")
        {
            var usuarios = _usuarioClient.ObtenerListaUsuario(page, pageSize, buscar);
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalCount"] = (usuarios != null && usuarios.Any()) ? usuarios.First().Total : 0;
            return PartialView("_TablaUsuarios", new ObservableCollection<Usuario>(usuarios));
        }
        public IActionResult Create()
        {
            UsuarioViewModels usuario = new UsuarioViewModels();
            usuario.Id = 0;
            usuario.Estado = true;
            usuario.menus = _menuClient.ObtenerListaMenu();
            return PartialView("_UsuarioForm", usuario);
        }
        [HttpPost]
        public IActionResult Create(UsuarioViewModels usuario)
        {

            usuario.menus = _menuClient.ObtenerListaMenu();
            if (ModelState.IsValid)
            {
          
                return Json(new { success = true });
            }
            return PartialView("_UsuarioForm", usuario);
        }
    }
}
