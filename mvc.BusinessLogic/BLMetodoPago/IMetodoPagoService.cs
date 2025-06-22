using SR.Entities.BaseEntities.MetodoPagoEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.BusinessLogic.BLMetodoPago
{
    public interface IMetodoPagoService
    {
        ObservableCollection<MetodoPago> ObtenerListaMetodoPago(int page, int pageSize, string buscar);
        bool GuardarMetodoPago(MetodoPago metodoPago);

        bool ValidarMetodoPagoNombre(string nombre, int Id);
        MetodoPago ObtenerMetodoPagoPorId(int Id);
        bool EliminarMetodoPagoPorId(int Id);
    }
}
