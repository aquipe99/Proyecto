using SR.Entities.BaseEntities.ReservaEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.BusinessLogic.BLReserva
{
    public interface IReservaService
    {
        Reserva ObtenerReservaPorId(int Id);
        bool GuardarReserva(Reserva reserva, out string mensaje);
        ObservableCollection<Reserva> ObtenerReservaPorFecha(DateTime Fecha);
        ObservableCollection<Reserva> ObtenerIngresoReservas(DateTime FechaIni, DateTime FechaFin, out decimal montoTotal, out int cantidadReservas, out int cantidadAnulados);
        ObservableCollection<Reserva> ObtenerMontoPorCancha(DateTime FechaIni, DateTime FechaFin, out decimal montoTotal, out int cantidadReservas);
        bool AnularReservaPorId(int Id);
    }
}
