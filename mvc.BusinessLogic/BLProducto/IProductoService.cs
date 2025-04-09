using mvc.Entities.ProductoEntites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.BusinessLogic.BLProducto
{
    public interface IProductoService
    {
        ObservableCollection<Producto> ObtenerTodos();
    }
}
