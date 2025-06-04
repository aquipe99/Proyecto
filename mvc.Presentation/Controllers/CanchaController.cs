using Microsoft.AspNetCore.Mvc;
using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.ViewModels;
using SR.ServiceClient.SCCancha;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace SR.Presentation.Controllers
{
    public class CanchaController : Controller
    {
        private readonly ICanchaClient _canchaClient;

        public CanchaController(ICanchaClient canchaClient) { 
            _canchaClient = canchaClient;
        }
        public IActionResult Index()
        {
            int page = 1; 
            int pageSize = 5;
            var canchas = _canchaClient.ObtenerListaCanchas(page, pageSize);
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalCount"] = (canchas != null && canchas.Any()) ? canchas.First().Total : 0;

            return View(canchas);
        }
        public IActionResult Paginar(int page = 1, int pageSize = 5)
        {
            var canchas = _canchaClient.ObtenerListaCanchas(page, pageSize);
            return PartialView("_TablaCanchas", new ObservableCollection<Cancha>(canchas));
        }
        public IActionResult Create()
        {
            return PartialView("_CanchaForm", new CanchaViewModel());
        }
        [HttpPost]
        public IActionResult Create(CanchaViewModel cancha)
        {

            if (_canchaClient.ValidarCanchaNombre(cancha.Nombre))
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
                _canchaClient.GuardarCancha(model);
                return Json(new { success = true });
            }
            return PartialView("_CanchaForm", cancha);
        }

        public IActionResult Edit(int id)
        {
            var cancha = _canchaClient.ObtenerCanchaPorId(id);
            if (cancha == null) return NotFound();
            return PartialView("_CanchaForm", cancha);
        }

        [HttpPost]
        public IActionResult Edit(Cancha cancha)
        {
            if (ModelState.IsValid)
            {
                _canchaClient.GuardarCancha(cancha);
                return RedirectToAction("Index");
            }
            return PartialView("_CanchaForm", cancha);
        }
    }
}
