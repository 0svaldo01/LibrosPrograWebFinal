using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddDbContext<LibrosPrograWebFinal.Models.LibreriaprograwebContext>(options => options.UseMySql("server=localhost;user=root;password=root;database=LibreriaPrograWeb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));

//AUNTENTIFICACION
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options=>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.MapAreaControllerRoute(
    name: "areas",
    areaName: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.Run();
  