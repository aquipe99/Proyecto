using Microsoft.AspNetCore.Mvc;
using mvc.ServiceClient.SCProducto;

namespace mvc.Presentation.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoClient _client;

        public ProductoController(IProductoClient productoClient) { 
            this._client = productoClient;
        }
        public IActionResult Index()
        {
            var productos = _client.ObtenerTodos();
            return View(productos);
        }
    }
}
