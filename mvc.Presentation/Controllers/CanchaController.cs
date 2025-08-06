using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.ViewModels;
using SR.ServiceClient.SCCancha;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace SR.Presentation.Controllers
{
    [Authorize]
    public class CanchaController : Controller
    {
        private readonly ICanchaClient _canchaClient;

        public CanchaController(ICanchaClient canchaClient) { 
            _canchaClient = canchaClient;
        }
        public IActionResult Index(int page = 1, int pageSize = 5,string buscar="")
        {
            var canchas = _canchaClient.ObtenerListaCanchas(page, pageSize, buscar);
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalCount"] = (canchas != null && canchas.Any()) ? canchas.First().Total : 0;
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_TablaCanchas", canchas);
            }
            return View(canchas);
        }
        public IActionResult Paginar(int page = 1, int pageSize = 5,string buscar="")
        {
           var canchas = _canchaClient.ObtenerListaCanchas(page, pageSize,buscar);
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalCount"] = (canchas != null && canchas.Any()) ? canchas.First().Total : 0;
            return PartialView("_TablaCanchas", new ObservableCollection<Cancha>(canchas));
        }
        public IActionResult Create()
        {
            return PartialView("_CanchaForm", new CanchaViewModel());
        }
        [HttpPost]
        public IActionResult Create(CanchaViewModel cancha)
        {

            if (_canchaClient.ValidarCanchaNombre(cancha.Nombre,cancha.Id))
            {
                ModelState.AddModelError("Nombre", "Ya existe una cancha con ese nombre.");
            }
            if (ModelState.IsValid)
            {
                var claims = HttpContext.User;
                var idClaim = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userId = int.TryParse(idClaim, out var idParsed) ? idParsed : 0;
                Cancha model = new Cancha();
                model.Nombre = cancha.Nombre;
                model.Descipcion = cancha.Descripcion;
                model.UsuarioModifica = userId;
                bool result= _canchaClient.GuardarCancha(model);
                return Json(new { success = result });
            }
            return PartialView("_CanchaForm", cancha);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _canchaClient.ObtenerCanchaPorId(id);
            if (model == null) return NotFound();
            CanchaViewModel cancha = new CanchaViewModel();
            cancha.Id = model.Id;
            cancha.Nombre = model.Nombre;
            cancha.Descripcion = model.Descipcion;
            cancha.Estado = model.Estado;
            return PartialView("_CanchaForm", cancha);
        }

        [HttpPost]
        public IActionResult Edit(CanchaViewModel cancha)
        {
            if (_canchaClient.ValidarCanchaNombre(cancha.Nombre,cancha.Id))
            {
                ModelState.AddModelError("Nombre", "Ya existe una cancha con ese nombre.");
            }
            if (ModelState.IsValid)
            {
                var claims = HttpContext.User;
                var idClaim = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userId = int.TryParse(idClaim, out var idParsed) ? idParsed : 0;
                Cancha model = new Cancha();
                model.Id=cancha.Id;
                model.Nombre = cancha.Nombre;
                model.Descipcion = cancha.Descripcion;
                model.UsuarioModifica = userId;
                bool result = _canchaClient.GuardarCancha(model);
                return Json(new { success = result });
            }
            return PartialView("_CanchaForm", cancha);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _canchaClient.ObtenerCanchaPorId(id);
            if (model == null) return NotFound();
            CanchaViewModel cancha = new CanchaViewModel();
            cancha.Id = model.Id;
            cancha.Nombre = model.Nombre;       
            return PartialView("_CanchaDelete", cancha);
        }

        [HttpPost]
        public IActionResult DeleteCancha(int id)
        {
            try {
                bool result = _canchaClient.EliminarCanchaPorId(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                
                if (ex is SqlException sqlEx && sqlEx.Number == 547)
                {                    
                    return Json(new { success = false, message = "No se puede eliminar la cancha porque tiene reservas asociadas." });
                }
          
                return Json(new { success = false, message = "Error al intentar eliminar la cancha: " + ex.Message });
            }


        }
    }
}
