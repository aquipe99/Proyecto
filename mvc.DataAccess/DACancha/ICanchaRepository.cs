using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.BaseEntities.MenuEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DataAccess.DACancha
{
    public interface ICanchaRepository
    {
        ObservableCollection<Cancha> ObtenerListaCanchas(int page, int pageSize, string buscar);
        bool GuardarCancha(Cancha cancha);

        bool ValidarCanchaNombre(string nombre);
        Cancha ObtenerCanchaPorId(int Id);
        bool EliminarCanchaPorId(int Id);
    }
}
