using SR.BusinessLogic.BLCancha;
using SR.BusinessLogic.BLMenu;
using SR.BusinessLogic.BLMetodoPago;
using SR.BusinessLogic.BLReserva;
using SR.BusinessLogic.BLUsuario;
using SR.DataAccess.DACancha;
using SR.DataAccess.DAMenu;
using SR.DataAccess.DAMetodoPago;
using SR.DataAccess.DAReserva;
using SR.DataAccess.DAUsuario;
using SR.ServiceClient.SCCancha;
using SR.ServiceClient.SCMenu;
using SR.ServiceClient.SCMetodoPago;
using SR.ServiceClient.SCReserva;
using SR.ServiceClient.SCUsuario;

namespace SR.Presentation.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services) {         
            //Repositorio
         
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<ICanchaRepository, CanchaRepository>();
            services.AddScoped<IMetodoPagoRepository, MetodoPagoRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();
            //service
           
            services.AddScoped<IUsuarioService,UsuarioService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<ICanchaService, CanchaService>();
            services.AddScoped<IMetodoPagoService, MetodoPagoService>();
            services.AddScoped<IReservaService, ReservaService>();
            //client
          
            services.AddScoped<IUsuarioClient, UsuarioClient>();
            services.AddScoped<IMenuClient, MenuClient>();
            services.AddScoped<ICanchaClient, CanchaClient>();
            services.AddScoped<IMetodoPagoClient, MetodoPagoClient>();
            services.AddScoped<IReservaClient, ReservaClient>();
        }
    }
}
