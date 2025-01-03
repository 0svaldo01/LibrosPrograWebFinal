using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddDbContext<LibrosPrograWebFinal.Models.LibreriaprograwebContext>(options => options.UseMySql("server=localhost;user=root;password=root;database=LibreriaPrograWeb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));


var app = builder.Build();

app.UseStaticFiles();
app.MapAreaControllerRoute(
    name: "areas",
    areaName: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapDefaultControllerRoute();
app.Run();
  