using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
            if (!string.IsNullOrWhiteSpace(usuario.Contrasenia))
            {
                if (usuario.Contrasenia.Length != 8)
                {
                    ModelState.AddModelError(nameof(usuario.Contrasenia), "La contraseña debe tener exactamente 8 caracteres.");
                }
            }
            if (usuario.Id == 0 && string.IsNullOrEmpty(usuario.Contrasenia))
            {
                ModelState.AddModelError(nameof(usuario.Contrasenia), "Contraseña es obligatoria.");
            }
            if (_usuarioClient.ValidarUsuarioCorreo(usuario.Correo,usuario.Id))
            {
                ModelState.AddModelError("Correo", "Ya existe una usuario con ese Correo.");
            }
            if (ModelState.IsValid)
            {
                var claims = HttpContext.User;
                var idClaim = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userId = int.TryParse(idClaim, out var idParsed) ? idParsed : 0;
                Usuario model = new Usuario();
                model.Nombre=usuario.Nombre;
                model.Telefono=usuario.Telefono;
                model.Correo=usuario.Correo;
                model.Contrasenia=usuario.Contrasenia;
                model.Rol_id=usuario.Rol_id;
                model.UsuarioCrea = userId;
                model.Estado=usuario.Estado;
                model.MenuSeleccionados = usuario.MenuSeleccionados;
                var result= _usuarioClient.SaveUsuario(model);            
                return Json(new { success = result });
            }
            usuario.menus = _menuClient.ObtenerListaMenu();
            return PartialView("_UsuarioForm", usuario);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _usuarioClient.ObtenerUsuarioPorId(id);
            if (model == null) return NotFound();
            UsuarioViewModels usuario = new UsuarioViewModels();
            usuario.Id = model.Id;
            usuario.Nombre = model.Nombre;
            usuario.Telefono = model.Telefono;
            usuario.Correo = model.Correo;           
            usuario.Rol_id = model.Rol_id;         
            usuario.Estado = model.Estado.Value;
            usuario.MenuSeleccionados = new List<int>(model.MenuSeleccionados);
            usuario.menus = _menuClient.ObtenerListaMenu();
            return PartialView("_UsuarioForm", usuario);
        }
        [HttpPost]
        public IActionResult Edit(UsuarioViewModels usuario)
        {
            if (!string.IsNullOrWhiteSpace(usuario.Contrasenia))
            {
                if (usuario.Contrasenia.Length != 8)
                {
                    ModelState.AddModelError(nameof(usuario.Contrasenia), "La contraseña debe tener exactamente 8 caracteres.");
                }            
            }
            if (_usuarioClient.ValidarUsuarioCorreo(usuario.Correo, usuario.Id))
            {
                ModelState.AddModelError("Correo", "Ya existe una usuario con ese Correo.");
            }
            if (ModelState.IsValid)
            {
                var claims = HttpContext.User;
                var idClaim = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userId = int.TryParse(idClaim, out var idParsed) ? idParsed : 0;
                Usuario model = new Usuario();
                model.Id = usuario.Id;
                model.Nombre = usuario.Nombre;
                model.Telefono = usuario.Telefono;
                model.Correo = usuario.Correo;
                model.Contrasenia = usuario.Contrasenia;
                model.Rol_id = usuario.Rol_id;
                model.UsuarioCrea = userId;
                model.Estado = usuario.Estado;
                model.MenuSeleccionados = usuario.MenuSeleccionados;
                var result = _usuarioClient.SaveUsuario(model);
                return Json(new { success = result });
            }
            usuario.menus = _menuClient.ObtenerListaMenu();
            return PartialView("_UsuarioForm", true);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _usuarioClient.ObtenerUsuarioPorId(id);
            if (model == null) return NotFound();
            UsuarioViewModels cancha = new UsuarioViewModels();
            cancha.Id = model.Id;
            cancha.Nombre = model.Nombre;
            return PartialView("_UsuarioDelete", cancha);
        }

        [HttpPost]
        public IActionResult DeleteUsuario(int id)
        {
            bool result = _usuarioClient.EliminarUsuarioPorId(id);
            return Json(new { success = result });

        }

    }
}
