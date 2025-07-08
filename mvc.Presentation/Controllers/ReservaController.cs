using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.BaseEntities.ReservaEntities;
using SR.Entities.ViewModels;
using SR.ServiceClient.SCCancha;
using SR.ServiceClient.SCMenu;
using SR.ServiceClient.SCMetodoPago;
using SR.ServiceClient.SCReserva;
using SR.ServiceClient.SCUsuario;
using System.Globalization;
using System.Reflection;
using System.Security.Claims;

namespace SR.Presentation.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ICanchaClient _canchaClient; 
        private readonly IMetodoPagoClient _metodoPagoClient;
        private readonly IReservaClient _reservaClient;

        public ReservaController(IReservaClient reservaClient,ICanchaClient canchaClient, IMetodoPagoClient metodoPagoClient)
        {
            _canchaClient = canchaClient;
            _metodoPagoClient = metodoPagoClient;
            _reservaClient=reservaClient;
        }
        public IActionResult Index()
        {
            var canchas = _canchaClient.ObtenerTodasLasCanchas(); 
            return View(canchas);           
        }

        [HttpGet]
        public IActionResult ObtenerReservasPorFecha(DateTime fecha)
        {
            var reservas = _reservaClient.ObtenerReservaPorFecha(fecha);
            var resultado = reservas.Select(r =>
            {
                decimal montoTotal = r.MontoTotal ?? 0;
                decimal montoPagado = r.MontoPagado ?? 0;
                decimal deuda = montoTotal - montoPagado;

                return new
                {
                    idReserva=r.Id,
                    canchaId = r.CanchaId.ToString(),
                    title = r.TipoPago == "parcial"
                        ? $"{r.NombreCliente} (Deuda: S/ {deuda.ToString("0.00", CultureInfo.InvariantCulture)})"
                        : r.NombreCliente,
                    start = $"{r.Fecha:yyyy-MM-dd}T{r.HoraInicio:hh\\:mm\\:ss}", 
                    end = $"{r.Fecha:yyyy-MM-dd}T{r.HoraFin:hh\\:mm\\:ss}",     
                    backgroundColor = r.TipoPago == "parcial" ? "yellow" : "green",
                    textColor = r.TipoPago == "parcial" ? "black" : "white",
                    borderColor = r.TipoPago == "parcial" ? "yellow" : "green"
                };
            });
            return Json(resultado);
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
            ValidarFecha(reserva.Fecha);
            ValidarHoras(reserva.Fecha, reserva.HoraInicio, reserva.HoraFin);
            ValidarPago(reserva.Estado, reserva.MontoPagado, reserva.MontoTotal);

            if (ModelState.IsValid)
            {
                string mensaje="";
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
                model.TipoPago = reserva.Estado == true ? "parcial" : "completo";
                model.MontoPagado = reserva.MontoPagado;
                model.UsuarioModifica = userId;
                bool result = _reservaClient.GuardarReserva(model,out mensaje);
                return Json(new { success = result, message = mensaje });
            }
            ViewBag.Canchas = new SelectList(reserva.Canchas, "Id", "Nombre");
            ViewBag.MetodoPagos = new SelectList(reserva.MetodoPagos, "Id", "Nombre");
            return PartialView("_ReservaForm", reserva);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
           
            var model = _reservaClient.ObtenerReservaPorId(id);
            if (model == null) return NotFound();

            ReservaViewModel reserva = new ReservaViewModel();
            reserva.Id=model.Id;
            reserva.NombreCliente = model.NombreCliente;
            reserva.Fecha = model.Fecha;
            reserva.HoraInicio = model.HoraInicio;
            reserva.HoraFin = model.HoraFin;
            reserva.CanchaId = model.CanchaId;
            reserva.Telefono = model.Telefono;
            reserva.MontoTotal = model.MontoTotal;
            reserva.MetodoPagoId = model.MetodoPagoId;
            reserva.Estado = model.TipoPago == "parcial" ? true : false;
            reserva.MontoPagado = model.MontoPagado;
            reserva.Canchas = _canchaClient.ObtenerTodasLasCanchas();
            reserva.MetodoPagos = _metodoPagoClient.ObtenerTodosLosMetodosPago();
            ViewBag.Canchas = new SelectList(reserva.Canchas, "Id", "Nombre");
            ViewBag.MetodoPagos = new SelectList(reserva.MetodoPagos, "Id", "Nombre");
            return PartialView("_ReservaForm", reserva);           
           
        }

        [HttpPost]
        public IActionResult Editar(ReservaViewModel reserva)
        {
            reserva.Canchas = _canchaClient.ObtenerTodasLasCanchas();
            reserva.MetodoPagos = _metodoPagoClient.ObtenerTodosLosMetodosPago();
            ValidarFecha(reserva.Fecha);
            ValidarHoras(reserva.Fecha, reserva.HoraInicio, reserva.HoraFin);
            ValidarPago(reserva.Estado, reserva.MontoPagado, reserva.MontoTotal);
            if (ModelState.IsValid)
            {
                string mensaje = "";
                var claims = HttpContext.User;
                var idClaim = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userId = int.TryParse(idClaim, out var idParsed) ? idParsed : 0;
                Reserva model = new Reserva();
                model.Id=reserva.Id;
                model.NombreCliente = reserva.NombreCliente;
                model.Fecha = reserva.Fecha;
                model.HoraInicio = reserva.HoraInicio;
                model.HoraFin = reserva.HoraFin;
                model.CanchaId = reserva.CanchaId;
                model.Telefono = reserva.Telefono;
                model.MontoTotal = reserva.MontoTotal;
                model.MetodoPagoId = reserva.MetodoPagoId;
                model.TipoPago = reserva.Estado == true ? "parcial" : "completo";
                model.MontoPagado = reserva.MontoPagado;
                model.UsuarioModifica = userId;
                bool result = _reservaClient.GuardarReserva(model, out mensaje);
                return Json(new { success = result });
            }
            return PartialView("_ReservaForm", reserva);
        }

        [HttpPost]
        public IActionResult Anular(int id)
        {          
            bool resultado = _reservaClient.AnularReservaPorId(id);
            return Json(new { success = true });
        }
        private void ValidarFecha(DateTime? fecha)
        {
            if (fecha < DateTime.Today)
            {
                ModelState.AddModelError("Fecha", "La fecha no puede ser menor a la fecha actual.");
            }
        }
        private void ValidarHoras(DateTime? fecha, TimeSpan? horaInicio, TimeSpan? horaFin)
        {
            TimeSpan horaMinima = new TimeSpan(7, 0, 0);
            TimeSpan horaMaxima = new TimeSpan(23, 0, 0);

            if (horaFin <= horaInicio)
            {
                ModelState.AddModelError("HoraFin", "La hora de fin debe ser mayor que la hora de inicio.");
            }

            if (horaInicio < horaMinima || horaInicio > horaMaxima)
            {
                ModelState.AddModelError("HoraInicio", "La hora de inicio debe estar entre las 07:00 y las 23:00.");
            }

            if (horaFin < horaMinima || horaFin > horaMaxima)
            {
                ModelState.AddModelError("HoraFin", "La hora de fin debe estar entre las 07:00 y las 23:00.");
            }

            if (fecha == DateTime.Today)
            {
                var horaActual = DateTime.Now.TimeOfDay;
                var horaActualConGracia = horaActual.Subtract(TimeSpan.FromMinutes(10)); // Tolerancia de 5 minutos

                if (horaInicio < horaActualConGracia)
                {
                    ModelState.AddModelError("HoraInicio", "La hora de inicio debe ser al menos 5 minutos mayor a la hora actual para reservas del mismo día.");
                }
            }
        }
        private void ValidarPago(bool estado, decimal? montoPagado, decimal? montoTotal)
        {
            if (estado)
            {
                if (montoPagado == null || montoPagado <= 0)
                {
                    ModelState.AddModelError("MontoPagado", "Debe ingresar un monto de adelanto válido.");
                }

                if (montoPagado > montoTotal)
                {
                    ModelState.AddModelError("MontoPagado", "El adelanto no puede ser mayor al monto total.");
                }
            }
        }

    }
}
