using SR.Entities.BaseEntities.CanchaEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.ServiceClient.SCCancha
{
    public interface ICanchaClient
    {
        bool ValidarCanchaNombre(string nombre,int Id);
        Cancha ObtenerCanchaPorId(int Id);
        bool GuardarCancha(Cancha cancha);
        ObservableCollection<Cancha> ObtenerListaCanchas(int page, int pageSize,string buscar);
        bool EliminarCanchaPorId(int Id);
        ObservableCollection<Cancha> ObtenerTodasLasCanchas();
    }
}
