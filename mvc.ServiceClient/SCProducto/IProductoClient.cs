using mvc.Entities.BaseEntities.ProductoEntites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.ServiceClient.SCProducto
{
    public interface IProductoClient
    {
        ObservableCollection<Producto> ObtenerTodos();
    }
}
