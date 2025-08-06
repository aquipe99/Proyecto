using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SR.Presentation.Helpers;
using SR.ServiceClient.SCReserva;
using System.Security.Claims;

namespace SR.Presentation.Controllers
{
    [Authorize]
    [AuthorizeUser]
    public class IngresoController : Controller
    {
        private readonly IReservaClient _reservaClient;

        public IngresoController(IReservaClient reservaClient) { 
            _reservaClient = reservaClient;
        }       
        public IActionResult Index()
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;            
            ViewData["UserRole"] = userRole;

            return View();
        }
        [HttpGet]
        public IActionResult IngresoReservas(DateTime fechaInicio, DateTime fechaFin)
        {
            var lista = _reservaClient.ObtenerIngresoReservas(fechaInicio, fechaFin, out decimal montoTotal, out int cantidadReservas,out int cantidadAnulados);
            return PartialView("_TablaReservas", lista);
        }
        [HttpGet]
        public IActionResult ObtenerTotalesReservas(DateTime fechaInicio, DateTime fechaFin)
        {
            _reservaClient.ObtenerIngresoReservas(fechaInicio, fechaFin, out decimal montoTotal, out int cantidadReservas, out int cantidadAnulados);
            return Json(new { montoTotal, cantidadReservas,cantidadAnulados });
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult ObtenerTotalesCanchas(DateTime fechaInicio, DateTime fechaFin)
        {
            _reservaClient.ObtenerMontoPorCancha(fechaInicio, fechaFin, out decimal montoTotal, out int cantidadReservas);
            return Json(new { montoTotal, cantidadReservas });
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult IngresoCanchas(DateTime fechaInicio, DateTime fechaFin)
        {
            var lista = _reservaClient.ObtenerMontoPorCancha(fechaInicio, fechaFin, out decimal montoTotal, out int cantidadReservas);
            return PartialView("_TablaCanchasIngresos", lista);
        }
    }
}
