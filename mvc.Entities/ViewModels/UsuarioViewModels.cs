using Microsoft.AspNetCore.Mvc.ModelBinding;
using SR.Entities.BaseEntities.MenuEntities;
using SR.Entities.BaseEntities.PermisoEntities;
using SR.Entities.BaseEntities.RolEntities;
using SR.Entities.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Entities.ViewModels
{
    public  class UsuarioViewModels
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(20, ErrorMessage = "El Nombre no puede tener más de 20 caracteres")]
        public string? Nombre { get; set; }
     
        [RegularExpression(@"^(9\d{8}|0\d{1,2}\d{6,7})$", ErrorMessage = "Debe ingresar un número válido de teléfono.")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        public string? Correo { get; set; }       
        [StringLength(8, MinimumLength = 8, ErrorMessage = "La contraseña debe tener exactamente 8 caracteres.")]
        public string? Contrasenia { get; set; }
        public Rol? Rol_id { get; set; }
        public Boolean Estado { get; set; }
        [BindNever]
        public ObservableCollection<Menu> menus { get; set; } = new ObservableCollection<Menu>();
        
        public bool EsAdminitrador
        {
            get => Rol_id?.Id == 1;
            set => Rol_id = new Rol { Id = value ? 1 : 2 };
        }
        public string? PermisosJson { get; set; }
        [RequerirAlMenosUnElemento(ErrorMessage = "Debe seleccionar al menos un permiso.")]
        public List<int> MenuSeleccionados { get; set; } = new();
    }
}
