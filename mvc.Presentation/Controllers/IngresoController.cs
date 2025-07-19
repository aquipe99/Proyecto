using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SR.ServiceClient.SCReserva;

namespace SR.Presentation.Controllers
{
    [Authorize]
    public class IngresoController : Controller
    {
        private readonly IReservaClient _reservaClient;

        public IngresoController(IReservaClient reservaClient) { 
            _reservaClient = reservaClient;
        }
        public IActionResult Index()
        {
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

        [HttpGet]
        public IActionResult ObtenerTotalesCanchas(DateTime fechaInicio, DateTime fechaFin)
        {
            _reservaClient.ObtenerMontoPorCancha(fechaInicio, fechaFin, out decimal montoTotal, out int cantidadReservas);
            return Json(new { montoTotal, cantidadReservas });
        }
        [HttpGet]
        public IActionResult IngresoCanchas(DateTime fechaInicio, DateTime fechaFin)
        {
            var lista = _reservaClient.ObtenerMontoPorCancha(fechaInicio, fechaFin, out decimal montoTotal, out int cantidadReservas);
            return PartialView("_TablaCanchasIngresos", lista);
        }
    }
}
