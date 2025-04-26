using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using examenparcialjj.Data;
using examenparcialjj.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!db.Teams.Any())
    {
        db.Teams.AddRange(
            new Team { Nombre = "Alianza Lima" },
            new Team { Nombre = "Universitario de Deportes" },
            new Team { Nombre = "Sporting Cristal" },
            new Team { Nombre = "Melgar" },
            new Team { Nombre = "Cienciano" },
            new Team { Nombre = "Sport Boys" },
            new Team { Nombre = "Carlos A. Mannucci" },
            new Team { Nombre = "UTC" },
            new Team { Nombre = "Binacional" },
            new Team { Nombre = "Cusco FC" },
            new Team { Nombre = "Alianza Atlético" },
            new Team { Nombre = "Atlético Grau" },
            new Team { Nombre = "Sport Huancayo" },
            new Team { Nombre = "Comerciantes Unidos" },
            new Team { Nombre = "Deportivo Garcilaso" }
        );
        db.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Players}/{action=Create}/{id?}");
app.MapRazorPages();

app.Run();
