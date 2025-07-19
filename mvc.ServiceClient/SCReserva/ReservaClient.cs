using SR.BusinessLogic.BLReserva;
using SR.Entities.BaseEntities.ReservaEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.ServiceClient.SCReserva
{
    public class ReservaClient : IReservaClient
    {
        public readonly IReservaService _reservaService;

        public ReservaClient(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        public bool AnularReservaPorId(int Id)
        {
            try
            {
                return _reservaService.AnularReservaPorId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public bool GuardarReserva(Reserva reserva, out string mensaje)
        {
            try
            {
                return _reservaService.GuardarReserva(reserva, out mensaje);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public ObservableCollection<Reserva> ObtenerIngresoReservas(DateTime FechaIni, DateTime FechaFin, out decimal montoTotal, out int cantidadReservas, out int cantidadAnulados)
        {
            try
            {
                return _reservaService.ObtenerIngresoReservas(FechaIni,FechaFin, out  montoTotal, out  cantidadReservas,out cantidadAnulados);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public ObservableCollection<Reserva> ObtenerMontoPorCancha(DateTime FechaIni, DateTime FechaFin, out decimal montoTotal, out int cantidadReservas)
        {
            try
            {
                return _reservaService.ObtenerMontoPorCancha(FechaIni, FechaFin, out montoTotal, out cantidadReservas);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public ObservableCollection<Reserva> ObtenerReservaPorFecha(DateTime Fecha)
        {
            try
            {
                return _reservaService.ObtenerReservaPorFecha(Fecha);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public Reserva ObtenerReservaPorId(int Id)
        {

            try
            {
                return _reservaService.ObtenerReservaPorId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
    }
}
