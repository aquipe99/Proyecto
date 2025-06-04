using Microsoft.AspNetCore.Identity;
using mvc.DataAccess.DAUsuario;
using mvc.Entities.BaseEntities.RolEntities;
using mvc.Entities.BaseEntities.UsuarioEntities;
using SR.Entities.BaseEntities.UsuarioEntities;
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

        public Usuario ValidarLogin(string correo, string contrasenia)
        {
            try
            {
                //var nuevoUsuario = new Usuario
                //{
                //    Nombre = "admin",
                //    Telefono = "999999999",
                //    Correo = "admin@gmail.com",                    
                //    Rol_id = new Rol { Id = 1 },
                //    UsuarioCrea = null,
                //    Estado = true
                //};
                //var passwordHasher = new PasswordHasher<Usuario>();
                //string passwordPlano = "SR@min123";
                //nuevoUsuario.Contrasenia = passwordHasher.HashPassword(nuevoUsuario, passwordPlano);
                //bool resul = _usuarioRepository.SaveUsaurio(nuevoUsuario);
       

                var usuario = _usuarioRepository.BuscarUsuarioPorCorreo(correo);
                if (usuario == null)
                {
                    return new Usuario
                    {
                        LoginResultado = LoginResultado.CredencialesIncorrectas
                    };
                }
                if (usuario.Estado ==false)
                {
                    usuario.LoginResultado = LoginResultado.UsuarioBloqueado;
                    return usuario;
                }
                var passwordHasher = new PasswordHasher<Usuario>();
                var resultadoPassword = passwordHasher.VerifyHashedPassword(usuario, usuario.Contrasenia, contrasenia);

                if (resultadoPassword == PasswordVerificationResult.Failed)
                {
                    usuario.LoginResultado = LoginResultado.CredencialesIncorrectas;
                    return usuario; 
                }
                usuario.LoginResultado = LoginResultado.Exito;
                return usuario;
            }
            catch (Exception ex) {
                throw new Exception("Error: ", ex);
            }
   
         
        }
    }
}
