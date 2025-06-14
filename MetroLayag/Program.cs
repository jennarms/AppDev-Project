using MetroLayag.Data;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddControllersWithViews(); // Required for ExportController

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

// Login redirect
app.Use(async (context, next) =>
{
    var isLoggedIn = context.Session.GetString("IsLoggedIn") == "true";
    if (!isLoggedIn && context.Request.Path == "/")
    {
        context.Response.Redirect("/LandingPage");
        return;
    }
    await next();
});

app.MapRazorPages();
app.MapControllers();

//Make sure the path below is correct
RotativaConfiguration.Setup(Path.Combine(builder.Environment.WebRootPath, "Rotativa"));

app.Run();
