
using mvc.Presentation.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.ConfigureServices();


var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.ConfigureRoutes();
app.Run();
