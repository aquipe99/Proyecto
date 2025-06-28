using SR.Entities.BaseEntities.RolEntities;
using SR.Entities.BaseEntities.UsuarioEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Entities.BaseEntities.UsuarioEntities
{
    public class Usuario
    {
        public int Id { get; set; } 
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; } 
        public string? Contrasenia { get; set; }
        public int Total { get; set; }
        public Rol? Rol_id { get; set; }
        public int? UsuarioCrea { get; set; }
        public int? UsuarioModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
        public Boolean? Estado { get; set; }
        public string? Estado_txt { get; set; }
        public LoginResultado LoginResultado { get; set; }
        public string? RolName { get; set; }
        public string? Titulo { get; set; }
        public List<int>? MenuSeleccionados { get; set; }
        public string? Permisos { get; set; }

    }
}
