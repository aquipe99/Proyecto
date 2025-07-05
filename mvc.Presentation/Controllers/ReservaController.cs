using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.BaseEntities.ReservaEntities;
using SR.Entities.ViewModels;
using SR.ServiceClient.SCCancha;
using SR.ServiceClient.SCMenu;
using SR.ServiceClient.SCMetodoPago;
using SR.ServiceClient.SCUsuario;
using System.Reflection;
using System.Security.Claims;

namespace SR.Presentation.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ICanchaClient _canchaClient; 
        private readonly IMetodoPagoClient _metodoPagoClient;

        public ReservaController(ICanchaClient canchaClient, IMetodoPagoClient metodoPagoClient)
        {
            _canchaClient = canchaClient;
            _metodoPagoClient = metodoPagoClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {

            var model = new ReservaViewModel
            {
                Fecha = DateTime.Today,
                HoraInicio = DateTime.Now.TimeOfDay,
                HoraFin = DateTime.Now.AddHours(1).TimeOfDay,
                Canchas= _canchaClient.ObtenerTodasLasCanchas(),
                MetodoPagos=_metodoPagoClient.ObtenerTodosLosMetodosPago()

            };
            ViewBag.Canchas = new SelectList(model.Canchas, "Id", "Nombre");
            ViewBag.MetodoPagos = new SelectList(model.MetodoPagos, "Id", "Nombre");
            return PartialView("_ReservaForm",model);
        }
        [HttpPost]
        public IActionResult Create(ReservaViewModel reserva)
        {
            reserva.Canchas = _canchaClient.ObtenerTodasLasCanchas();
            reserva.MetodoPagos = _metodoPagoClient.ObtenerTodosLosMetodosPago();
            if (reserva.Estado) // Solo si quiere adelanto
            {
                if (reserva.MontoPagado == null || reserva.MontoPagado <= 0)
                {
                    ModelState.AddModelError("MontoPagado", "Debe ingresar un monto de adelanto válido.");
                }

                if (reserva.MontoPagado > reserva.MontoTotal)
                {
                    ModelState.AddModelError("MontoPagado", "El adelanto no puede ser mayor al monto total.");
                }
            }

            if (ModelState.IsValid)
            {
                var claims = HttpContext.User;
                var idClaim = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userId = int.TryParse(idClaim, out var idParsed) ? idParsed : 0;
                Reserva model = new Reserva();
                model.NombreCliente = reserva.NombreCliente;
                model.Fecha = reserva.Fecha;
                model.HoraInicio = reserva.HoraInicio;
                model.HoraFin = reserva.HoraFin;
                model.CanchaId = reserva.CanchaId;
                model.Telefono = reserva.Telefono;
                model.MontoTotal = reserva.MontoTotal;
                model.MetodoPagoId = reserva.MetodoPagoId;
                model.TipoPago = reserva.Estado == true ? "completo" : "parcial";
                model.MontoPagado = reserva.MontoPagado;
                model.UsuarioModifica = userId;
                bool result = _reservaClient.GuardarReserva(model);
                return Json(new { success = true });
            }
            ViewBag.Canchas = new SelectList(reserva.Canchas, "Id", "Nombre");
            ViewBag.MetodoPagos = new SelectList(reserva.MetodoPagos, "Id", "Nombre");
            return PartialView("_ReservaForm", reserva);
        }
    }
}
