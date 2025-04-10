using mvc.BusinessLogic.BLProducto;
using mvc.BusinessLogic.BLUsuario;
using mvc.DataAccess.DAProducto;
using mvc.DataAccess.DAUsuario;
using mvc.ServiceClient.SCProducto;
using mvc.ServiceClient.SCUsuario;

namespace mvc.Presentation.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services) {         
            //Repositorio
            services.AddScoped<IProductoRepository, ProductoRespository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            //service
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IUsuarioService,UsuarioService>();

            //client
            services.AddScoped<IProductoClient, ProductoClient>();
            services.AddScoped<IUsuarioClient, UsuarioClient>();
        }
    }
}
