using mvc.Entities.BaseEntities.UsuarioEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.DataAccess.DAUsuario
{
    public interface IUsuarioRepository
    {
        Usuario ObtenerPorUsuarioGmail(string gmail);
    }
}
