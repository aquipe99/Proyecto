using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Entities.BaseEntities.CanchaEntities
{
    public class Cancha
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descipcion { get; set; }
        public int Total {  get; set; }
        public int? UsuarioModifica { get; set; }
        public Boolean? Estado { get; set; }

    }
}
