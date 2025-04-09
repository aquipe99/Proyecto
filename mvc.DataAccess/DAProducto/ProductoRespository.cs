using mvc.Entities.ProductoEntites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.DataAccess.DAProducto
{
    public class ProductoRespository : IProductoRepository
    {
        public ObservableCollection<Producto> ObtenerTodos()
        {
            return new ObservableCollection<Producto>
            {
                 new Producto { Id=1,Nombre="Helado",Precio =5.5m},
                 new Producto { Id=2,Nombre="Torta",Precio= 12.0m }
            };
        }
    }
}
