using SR.DataAccess.DAReserva;
using SR.Entities.BaseEntities.ReservaEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.BusinessLogic.BLReserva
{
    public class ReservaService : IReservaService
    {
        public readonly IReservaRepository _reservaRepository;

        public ReservaService(IReservaRepository reservaRepository) { 
            _reservaRepository = reservaRepository;
        }
        public bool GuardarReserva(Reserva reserva, out string mensaje)
        {
            try
            {
                return _reservaRepository.GuardarReserva(reserva,out mensaje);
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
                return _reservaRepository.ObtenerReservaPorFecha(Fecha);
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
                return _reservaRepository.ObtenerReservaPorId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
    }
}
