using SR.Entities.BaseEntities.PermisoEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Entities.BaseEntities.MenuEntities
{
    public class Menu
    {
        public int Id { get; set; }
        public string? Descipcion { get; set; }
        public string? Link { get; set; }
        public string? Icono { get; set; }
        public int? Menupadre { get; set; }
        public int? OrdenMenu { get; set; }
    }
}
