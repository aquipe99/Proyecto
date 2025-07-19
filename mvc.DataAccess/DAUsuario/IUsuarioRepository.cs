using SR.Entities.BaseEntities.UsuarioEntities;
using SR.Entities.BaseEntities.MetodoPagoEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DataAccess.DAUsuario
{
    public interface IUsuarioRepository
    {
        Usuario ObtenerPorUsuarioGmail(string correo);
        Usuario BuscarUsuarioPorCorreo(string correo);
        bool SaveUsuario(Usuario usuario);
        Usuario ObtenerUsuarioPorId(int Id);
        bool ValidarUsuarioCorreo(string correo, int Id);
        ObservableCollection<Usuario> ObtenerListaUsuario(int page, int pageSize, string buscar);
        bool EliminarUsuarioPorId(int Id);
    }
}
