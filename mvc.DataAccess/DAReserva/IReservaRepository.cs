using SR.Entities.BaseEntities.ReservaEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DataAccess.DAReserva
{
    public interface IReservaRepository
    {
        bool GuardarReserva(Reserva reserva, out string mensaje);
    }
}
