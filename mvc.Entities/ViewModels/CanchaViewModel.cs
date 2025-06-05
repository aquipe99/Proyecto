using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Entities.ViewModels
{
    public class CanchaViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(20, ErrorMessage = "El Nombre no puede tener más de 20 caracteres")]
        public string? Nombre { get; set; }
        [StringLength(20, ErrorMessage = "El Nombre no puede tener más de 20 caracteres")]
        public string? Descripcion { get; set; }
        public Boolean? Estado { get; set; }
    }
}
