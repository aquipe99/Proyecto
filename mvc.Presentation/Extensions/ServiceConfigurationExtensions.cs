using mvc.BusinessLogic.BLProducto;
using mvc.DataAccess.DAProducto;
using mvc.ServiceClient.SCProducto;

namespace mvc.Presentation.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services) {
            services.AddScoped<IProductoRepository, ProductoRespository>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IProductoClient, ProductoClient>();
        }
    }
}
