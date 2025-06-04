using mvc.Entities.BaseEntities.UsuarioEntities;
using SR.Entities.BaseEntities.UsuarioEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.BusinessLogic.BLUsuario
{
    public interface IUsuarioService
    {
        Usuario ValidarLogin(string correo, string contrasenia);
    }
}
