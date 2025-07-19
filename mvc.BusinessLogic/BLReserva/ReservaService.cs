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

        public bool AnularReservaPorId(int Id)
        {
            try
            {
                return _reservaRepository.AnularReservaPorId(Id);
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
                if (reserva.MontoTotal == reserva.MontoPagado && reserva.TipoPago== "parcial") {
                    reserva.TipoPago = "completo";
                }
                if (reserva.TipoPago == "completo" && reserva.MontoPagado!= null)
                {
                    reserva.MontoPagado = null;
                    reserva.TipoPago = "completo";
                }
                return _reservaRepository.GuardarReserva(reserva,out mensaje);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public ObservableCollection<Reserva> ObtenerIngresoReservas(DateTime FechaIni, DateTime FechaFin, out decimal montoTotal, out int cantidadReservas,out int cantidadAnulados)
        {
            try
            {
                var reservas = _reservaRepository.ObtenerIngresoReservas(FechaIni, FechaFin);
                montoTotal = 0;
                cantidadReservas = 0;
                cantidadAnulados = 0;

                if (reservas != null)
                {
                    foreach (var reserva in reservas)
                    {
                        var tipo = reserva.TipoPago?.ToLowerInvariant();

                        if (tipo == "completo")
                        {
                            montoTotal += reserva.MontoTotal ?? 0;
                            cantidadReservas++;
                        }
                        else if (tipo == "parcial")
                        {
                            montoTotal += reserva.MontoPagado ?? 0;
                            cantidadReservas++;
                        }
                        else if (tipo == "anulado")
                        { 
                         cantidadAnulados++;
                        }
                    }
                }
                
                return reservas;
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
                montoTotal = 0;
                var reservas = _reservaRepository.ObtenerMontoPorCancha(FechaIni, FechaFin, out cantidadReservas);
                montoTotal = reservas.Sum(r => r.MontoTotal ?? 0);
                return reservas;
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
