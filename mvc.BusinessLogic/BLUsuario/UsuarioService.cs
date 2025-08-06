using Azure;
using Microsoft.AspNetCore.Identity;
using SR.DataAccess.DAUsuario;
using SR.Entities.BaseEntities.MenuEntities;
using SR.Entities.BaseEntities.UsuarioEntities;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace SR.BusinessLogic.BLUsuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public bool EliminarUsuarioPorId(int Id)
        {
            try
            {
                return _usuarioRepository.EliminarUsuarioPorId(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ObservableCollection<Usuario> ObtenerListaUsuario(int page, int pageSize, string buscar)
        {
            try
            {
                return _usuarioRepository.ObtenerListaUsuario(page, pageSize, buscar);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public Usuario ObtenerUsuarioPorId(int Id)
        {
            try
            {
                return _usuarioRepository.ObtenerUsuarioPorId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public bool SaveUsuario(Usuario usuario)
        {
            try {
                if (!string.IsNullOrWhiteSpace(usuario.Contrasenia))
                {
                    var passwordHasher = new PasswordHasher<Usuario>();
                    usuario.Contrasenia = passwordHasher.HashPassword(usuario, usuario.Contrasenia);
                }
                else {
                    usuario.Contrasenia = null;
                }
                usuario.PermisosJson = JsonSerializer.Serialize(usuario.Permisos.Select(p => new {Menu = p.MenuId }));                
                bool resul = _usuarioRepository.SaveUsuario(usuario);
                return resul;
            }
            catch (Exception ex) {
                throw new Exception("Error: ", ex);
            }
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

        public bool ValidarUsuarioCorreo(string correo, int Id)
        {
            try
            {
                return _usuarioRepository.ValidarUsuarioCorreo(correo,Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
    }
}
