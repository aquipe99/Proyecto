using mvc.DataAccess.DAProducto;
using mvc.Entities.ProductoEntites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.BusinessLogic.BLProducto
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repo) { 
            this._repository = repo;
        }

        public ObservableCollection<Producto> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }
    }
}
