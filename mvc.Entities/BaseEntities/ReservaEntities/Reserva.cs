using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Entities.BaseEntities.ReservaEntities
{
    public class Reserva
    {
        public int Id { get; set; }

        public string? NombreCliente { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan? HoraInicio { get; set; }

        public TimeSpan? HoraFin { get; set; }

        public int? MetodoPagoId { get; set; }

        public string?TipoPago { get; set; }

        public int? CanchaId { get; set; }

        public decimal? MontoTotal { get; set; }

        public decimal? MontoPagado { get; set; }

        public string? Telefono { get; set; }
        public int? UsuarioModifica { get; set; }
    }
}
