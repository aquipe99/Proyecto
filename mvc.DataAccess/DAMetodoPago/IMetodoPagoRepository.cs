using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.BaseEntities.MetodoPagoEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DataAccess.DAMetodoPago
{
    public interface IMetodoPagoRepository
    {
        ObservableCollection<MetodoPago> ObtenerListaMetodoPago(int page, int pageSize, string buscar);
        bool GuardarMetodoPago(MetodoPago metodoPago);
        ObservableCollection<MetodoPago> ObtenerTodosLosMetodosPago();
        bool ValidarMetodoPagoNombre(string nombre, int Id);
        MetodoPago ObtenerMetodoPagoPorId(int Id);
        bool EliminarMetodoPagoPorId(int Id);
    }
}
