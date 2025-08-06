namespace SR.Presentation.Extensions
{
    public static class RouteConfigurationExtensions
    {
        public static void ConfigureRoutes(this WebApplication app) {
             app.MapControllerRoute(
             name: "default",
             pattern: "{controller=Login}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "error404",
                pattern: "Error404",
                defaults: new { controller = "Error", action = "Error404" });
        }
    }
}
