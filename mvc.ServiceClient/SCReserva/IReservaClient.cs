using SR.Entities.BaseEntities.ReservaEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.ServiceClient.SCReserva
{
    public interface IReservaClient
    {
        Reserva ObtenerReservaPorId(int Id);
        bool GuardarReserva(Reserva reserva, out string mensaje);
        ObservableCollection<Reserva> ObtenerReservaPorFecha(DateTime Fecha);
    }
}
