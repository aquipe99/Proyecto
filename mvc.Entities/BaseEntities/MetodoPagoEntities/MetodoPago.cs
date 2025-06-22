using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Entities.BaseEntities.MetodoPagoEntities
{
    public class MetodoPago
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }       
        public int Total { get; set; }
        public int? UsuarioModifica { get; set; }       
    }
}
