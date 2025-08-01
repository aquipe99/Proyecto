﻿using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.BaseEntities.ReservaEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DataAccess.DAReserva
{
    public interface IReservaRepository
    {
        Reserva ObtenerReservaPorId(int Id);
        bool GuardarReserva(Reserva reserva, out string mensaje);
        ObservableCollection<Reserva>ObtenerReservaPorFecha(DateTime Fecha);
        ObservableCollection<Reserva> ObtenerIngresoReservas(DateTime FechaIni, DateTime FechaFin);
        ObservableCollection<Reserva> ObtenerMontoPorCancha(DateTime FechaIni, DateTime FechaFin,out int cantidadReservas);
        bool AnularReservaPorId(int Id);
    }
}
