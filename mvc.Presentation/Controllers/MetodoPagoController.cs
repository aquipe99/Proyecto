using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.BaseEntities.MetodoPagoEntities;
using SR.Entities.ViewModels;
using SR.Presentation.Helpers;
using SR.ServiceClient.SCMetodoPago;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace SR.Presentation.Controllers
{
    [Authorize]
    [AuthorizeUser]
    public class MetodoPagoController : Controller
    {
        private readonly IMetodoPagoClient _metodoPagoClient;
        public MetodoPagoController(IMetodoPagoClient metodoPagoClient)
        {
            _metodoPagoClient = metodoPagoClient;
        }       
        public IActionResult Index(int page = 1, int pageSize = 5, string buscar = "")
        {
            var metodoPagos = _metodoPagoClient.ObtenerListaMetodoPago(page, pageSize, buscar);
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalCount"] = (metodoPagos != null && metodoPagos.Any()) ? metodoPagos.First().Total : 0;
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_TablaMedotoPago", metodoPagos);
            }
            return View(metodoPagos);
        }
        public IActionResult Paginar(int page = 1, int pageSize = 5, string buscar = "")
        {
            var metodoPagos = _metodoPagoClient.ObtenerListaMetodoPago(page, pageSize, buscar);
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalCount"] = (metodoPagos != null && metodoPagos.Any()) ? metodoPagos.First().Total : 0;
            return PartialView("_TablaMedotoPago", new ObservableCollection<MetodoPago>(metodoPagos));
        }
     
        public IActionResult Create()
        {
            return PartialView("_MetodoPagoForm", new MetodoPagoViewModel());
        }
        [HttpPost]
        public IActionResult Create(MetodoPagoViewModel metodoPago)
        {

            if (_metodoPagoClient.ValidarMetodoPagoNombre(metodoPago.Nombre, metodoPago.Id))
            {
                ModelState.AddModelError("Nombre", "Ya existe una metodo de pago con ese nombre.");
            }
            if (ModelState.IsValid)
            {
                var claims = HttpContext.User;
                var idClaim = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userId = int.TryParse(idClaim, out var idParsed) ? idParsed : 0;
                MetodoPago model = new MetodoPago();
                model.Nombre = metodoPago.Nombre;           
                model.UsuarioModifica = userId;
                bool result= _metodoPagoClient.GuardarMetodoPago(model);
                return Json(new { success = true });
            }
            return PartialView("_MetodoPagoForm", metodoPago);
        }
  
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _metodoPagoClient.ObtenerMetodoPagoPorId(id);
            if (model == null) return NotFound();
            MetodoPagoViewModel metodoPago = new MetodoPagoViewModel();
            metodoPago.Id = model.Id;
            metodoPago.Nombre = model.Nombre;      
            return PartialView("_MetodoPagoForm", metodoPago);
        }

        [HttpPost]
        public IActionResult Edit(MetodoPagoViewModel metodoPago)
        {
            if (_metodoPagoClient.ValidarMetodoPagoNombre(metodoPago.Nombre, metodoPago.Id))
            {
                ModelState.AddModelError("Nombre", "Ya existe una metodo de pago con ese nombre.");
            }
            if (ModelState.IsValid)
            {
                var claims = HttpContext.User;
                var idClaim = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userId = int.TryParse(idClaim, out var idParsed) ? idParsed : 0;
                MetodoPago model = new MetodoPago();
                model.Id = metodoPago.Id;
                model.Nombre = metodoPago.Nombre;               
                model.UsuarioModifica = userId;
                bool result = _metodoPagoClient.GuardarMetodoPago(model);
                return Json(new { success = true });
            }
            return PartialView("_MetodoPagoForm", metodoPago);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _metodoPagoClient.ObtenerMetodoPagoPorId(id);
            if (model == null) return NotFound();
            MetodoPagoViewModel metodoPago = new MetodoPagoViewModel();
            metodoPago.Id = model.Id;
            metodoPago.Nombre = model.Nombre;
            return PartialView("_MetodoPagoDelete", metodoPago);
        }

        [HttpPost]
        public IActionResult DeleteMetodoPago(int id)
        {
            try {
                bool result = _metodoPagoClient.EliminarMetodoPagoPorId(id);
                return Json(new { success = true });
            }     
             catch (Exception ex)
            {

                if (ex is SqlException sqlEx && sqlEx.Number == 547)
                {
                    return Json(new { success = false, message = "No se puede eliminar el metodo de pago porque tiene reservas asociadas." });
                }
                return Json(new { success = false, message = "Error al intentar eliminar el metodo de pago: " + ex.Message });
            }

        }
    }
}
