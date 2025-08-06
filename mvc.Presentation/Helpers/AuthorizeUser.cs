using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SR.ServiceClient.SCMenu;
using System.Security.Claims;

namespace SR.Presentation.Helpers
{
    public class AuthorizeUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var requestedUrl = context.HttpContext.Request.Path.Value.ToLowerInvariant();  // URL a la que el usuario intenta acceder

            // Acceder a la inyección de dependencias del servicio IMenuClient
            var menuClient = (IMenuClient)context.HttpContext.RequestServices.GetService(typeof(IMenuClient));

            if (menuClient == null)
            {
                // Si no se pudo obtener el servicio, lanzar un error o manejarlo
                context.Result = new RedirectToRouteResult(new Microsoft.AspNetCore.Routing.RouteValueDictionary
                {
                    { "controller", "Error" },
                    { "action", "Error403" }
                });
                return;
            }

            // Obtener los menús del usuario
            var userMenus = menuClient.ObtenerMenuPorusuario(userId);
            Console.WriteLine($"Menús del usuario {userId}: {string.Join(", ", userMenus.Select(m => m.Link))}");

            var requestedController = requestedUrl.Split('/').Skip(1).FirstOrDefault();
            // Verificar si el usuario tiene acceso a este menú
            var menu = userMenus.FirstOrDefault(m => m.Link?.ToLowerInvariant() == $"/{requestedController}");
            if (menu == null)
            {
                // Si el usuario no tiene permiso para este menú, redirigir a la página de "No autorizado"
                context.Result = new RedirectToRouteResult(new Microsoft.AspNetCore.Routing.RouteValueDictionary
                {
                    { "controller", "Error" },
                    { "action", "Error403" }
                });
            }

            base.OnActionExecuting(context);
        }
    }
}
