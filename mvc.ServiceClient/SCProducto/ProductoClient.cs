using mvc.BusinessLogic.BLProducto;
using mvc.Entities.BaseEntities.ProductoEntites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.ServiceClient.SCProducto
{
    public class ProductoClient : IProductoClient
    {
        public readonly IProductoService _productoService;

        public ProductoClient(IProductoService productoService) { 
            this._productoService = productoService;
        }
        public ObservableCollection<Producto> ObtenerTodos()
        {
            return _productoService.ObtenerTodos();
        }
    }
}
