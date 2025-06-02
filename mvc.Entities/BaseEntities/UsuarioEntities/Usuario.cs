using mvc.Entities.BaseEntities.RolEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.Entities.BaseEntities.UsuarioEntities
{
    public partial class Usuario
    {
        public int Id { get; set; } 
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; } 
        public string? Contrasenia { get; set; }
        public Rol? Rol_id { get; set; }
        public DateTime? UsuarioCrea { get; set; }
        public int? UsuarioModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
        public Boolean? Estado { get; set; }

    }
}
