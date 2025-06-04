using mvc.BusinessLogic.BLProducto;
using mvc.BusinessLogic.BLUsuario;
using mvc.DataAccess.DAProducto;
using mvc.DataAccess.DAUsuario;
using mvc.ServiceClient.SCProducto;
using mvc.ServiceClient.SCUsuario;
using SR.BusinessLogic.BLMenu;
using SR.DataAccess.DAMenu;
using SR.ServiceClient.SCMenu;

namespace mvc.Presentation.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services) {         
            //Repositorio
            services.AddScoped<IProductoRepository, ProductoRespository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            //service
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IUsuarioService,UsuarioService>();
            services.AddScoped<IMenuService, MenuService>();

            //client
            services.AddScoped<IProductoClient, ProductoClient>();
            services.AddScoped<IUsuarioClient, UsuarioClient>();
            services.AddScoped<IMenuClient, MenuClient>();
        }
    }
}
