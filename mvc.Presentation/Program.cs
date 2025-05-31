
using mvc.DataAccess.Servicios;
using mvc.Presentation.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews()
    .AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled = true)
    .AddDataAnnotationsLocalization();
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/Logout";
    });
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<ConnectionService>();
builder.Services.ConfigureServices();



var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.ConfigureRoutes();
app.Run();
