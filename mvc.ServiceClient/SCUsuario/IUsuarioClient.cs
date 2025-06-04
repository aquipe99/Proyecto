using mvc.Entities.BaseEntities.UsuarioEntities;
using SR.Entities.BaseEntities.MenuEntities;
using SR.Entities.BaseEntities.UsuarioEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.ServiceClient.SCUsuario
{
    public interface IUsuarioClient
    {
        Usuario ValidarLogin(string correo, string contrasenia);      
    }
}
