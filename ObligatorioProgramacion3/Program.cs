using Microsoft.EntityFrameworkCore;
using ObligatorioProgramacion3.Datos;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la conexión a SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL"))
);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el pipeline de manejo de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    // Configuración del manejador de excepciones para entornos que no son de desarrollo.
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es 30 días. Puede que desees cambiar esto para escenarios de producción, ver https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Redirección de HTTP a HTTPS
app.UseHttpsRedirection();
// Habilitar el uso de archivos estáticos (CSS, JavaScript, imágenes, etc.)
app.UseStaticFiles();

// Configuración del enrutamiento
app.UseRouting();

// Configuración de la autorización
app.UseAuthorization();

// Definición de las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ejecutar la aplicación
app.Run();
