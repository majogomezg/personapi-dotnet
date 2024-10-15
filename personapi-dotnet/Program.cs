using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Repositories;
using personapi_dotnet.Models.Repositories.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PersonaDbContext>(options =>
{
    // Add Connection string from appsettings.json
    var defatulConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(defatulConnectionString);
});


// Register repository
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
