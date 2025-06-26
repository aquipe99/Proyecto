using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Entities.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="El correo es obligatorio")]
        [EmailAddress(ErrorMessage ="Ingrese un correo válido")]
        public string? correo { get; set; }

        [Required(ErrorMessage ="La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string? contrasenia { get; set; }
    }
}
