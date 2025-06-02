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
        LoginResultado ValidarLogin(string correo, string contrasenia);
    }
}
