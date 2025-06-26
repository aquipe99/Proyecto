using SR.Entities.BaseEntities.UsuarioEntities;
using SR.Entities.BaseEntities.MenuEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.ServiceClient.SCUsuario
{
    public interface IUsuarioClient
    {
        ObservableCollection<Usuario> ObtenerListaUsuario(int page, int pageSize, string buscar);
        Usuario ValidarLogin(string correo, string contrasenia);      
    }
}
