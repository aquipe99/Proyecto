using SR.Entities.BaseEntities.MenuEntities;
using SR.Entities.BaseEntities.UsuarioEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Entities.BaseEntities.PermisoEntities
{
    public class Permiso
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int MenuId { get; set; }
    }
}
