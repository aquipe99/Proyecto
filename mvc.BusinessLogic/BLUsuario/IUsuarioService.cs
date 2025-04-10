using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.BusinessLogic.BLUsuario
{
    public interface IUsuarioService
    {
        bool ValidarLogin(string gmail, string contrasenia);
    }
}
