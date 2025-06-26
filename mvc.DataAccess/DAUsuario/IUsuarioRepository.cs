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
        Usuario ObtenerPorUsuarioGmail(string gmail);
        Usuario BuscarUsuarioPorCorreo(string correo);
        bool SaveUsaurio(Usuario usuario);
        ObservableCollection<Usuario> ObtenerListaUsuario(int page, int pageSize, string buscar);
    }
}
