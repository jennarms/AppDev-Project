using MetroLayag.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();



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
app.Run();