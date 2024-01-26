using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
var builder = WebApplication.CreateBuilder();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 21))));
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseExceptionHandler("/Error");
app.UseHsts();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Map("/Error", app => app.Run(async context =>
{
    context.Response.StatusCode = 400;
    await context.Response.WriteAsync("ERROR! NOT ALL FIELDS ARE FILLED!");
}));

app.Run();