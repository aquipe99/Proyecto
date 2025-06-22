using mvc.BusinessLogic.BLProducto;
using mvc.BusinessLogic.BLUsuario;
using mvc.DataAccess.DAProducto;
using mvc.DataAccess.DAUsuario;
using mvc.ServiceClient.SCProducto;
using mvc.ServiceClient.SCUsuario;
using SR.BusinessLogic.BLCancha;
using SR.BusinessLogic.BLMenu;
using SR.BusinessLogic.BLMetodoPago;
using SR.DataAccess.DACancha;
using SR.DataAccess.DAMenu;
using SR.DataAccess.DAMetodoPago;
using SR.ServiceClient.SCCancha;
using SR.ServiceClient.SCMenu;
using SR.ServiceClient.SCMetodoPago;

namespace mvc.Presentation.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services) {         
            //Repositorio
            services.AddScoped<IProductoRepository, ProductoRespository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<ICanchaRepository, CanchaRepository>();
            services.AddScoped<IMetodoPagoRepository, MetodoPagoRepository>();
            //service
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IUsuarioService,UsuarioService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<ICanchaService, CanchaService>();
            services.AddScoped<IMetodoPagoService, MetodoPagoService>();
            //client
            services.AddScoped<IProductoClient, ProductoClient>();
            services.AddScoped<IUsuarioClient, UsuarioClient>();
            services.AddScoped<IMenuClient, MenuClient>();
            services.AddScoped<ICanchaClient, CanchaClient>();
            services.AddScoped<IMetodoPagoClient, MetodoPagoClient>();
        }
    }
}
