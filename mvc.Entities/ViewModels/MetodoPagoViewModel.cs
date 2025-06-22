using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Entities.ViewModels
{
    public class MetodoPagoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El Nombre no puede tener más de 50 caracteres")]
        public string? Nombre { get; set; }
    
    }
}
