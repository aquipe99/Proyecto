namespace mvc.Presentation.Extensions
{
    public static class RouteConfigurationExtensions
    {
        public static void ConfigureRoutes(this WebApplication app) {
            app.MapControllerRoute(
         name: "Producto",
         pattern: "{controller=Producto}/{action=Index}");
        }
    }
}
