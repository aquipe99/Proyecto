using mvc.BusinessLogic.BLUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.ServiceClient.SCUsuario
{
    public class UsuarioClient : IUsuarioClient
    {
        private readonly IUsuarioService _usuarioservice;

        public UsuarioClient(IUsuarioService usuarioservice)
        {
            _usuarioservice = usuarioservice;
        }

        public bool ValidarLogin(string gmail, string contrasenia)
        {
            return _usuarioservice.ValidarLogin(gmail, contrasenia);
        }
    }
}
