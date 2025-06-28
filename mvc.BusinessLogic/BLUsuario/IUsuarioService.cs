using SR.Entities.BaseEntities.UsuarioEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.BusinessLogic.BLUsuario
{
    public interface IUsuarioService
    {
        Usuario ValidarLogin(string correo, string contrasenia);
        ObservableCollection<Usuario> ObtenerListaUsuario(int page, int pageSize, string buscar);
        bool SaveUsuario(Usuario usuario);
        Usuario ObtenerUsuarioPorId(int Id);
        bool ValidarUsuarioCorreo(string correo, int Id);
        bool EliminarUsuarioPorId(int Id);
    }
}
