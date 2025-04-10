using mvc.DataAccess.DAUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.BusinessLogic.BLUsuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public bool ValidarLogin(string gmail, string contrasenia)
        {
            var usuario = _usuarioRepository.ObtenerPorUsuarioGmail(gmail);
            if(usuario == null || string.IsNullOrEmpty(usuario.Correo)) return false;
            return usuario.Contrasenia==contrasenia;
        }
    }
}
