using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.Entities.UsuarioEntities
{
    public partial class Usuario
    {
        public int Id { get; set; } 
        public string? Nombre { get; set; } 
        public string? Correo { get; set; } 
        public string? Contrasenia { get; set; } 
        public DateTime? UsuarioCrea { get; set; }
        public int? UsuarioModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
        public string? Activo { get; set; }

    }
}
