using mvc.BusinessLogic.BLProducto;

using mvc.DataAccess.DAProducto;

using mvc.ServiceClient.SCProducto;

using SR.BusinessLogic.BLCancha;
using SR.BusinessLogic.BLMenu;
using SR.BusinessLogic.BLMetodoPago;
using SR.BusinessLogic.BLUsuario;
using SR.DataAccess.DACancha;
using SR.DataAccess.DAMenu;
using SR.DataAccess.DAMetodoPago;
using SR.DataAccess.DAUsuario;
using SR.ServiceClient.SCCancha;
using SR.ServiceClient.SCMenu;
using SR.ServiceClient.SCMetodoPago;
using SR.ServiceClient.SCUsuario;

namespace SR.Presentation.Extensions
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
