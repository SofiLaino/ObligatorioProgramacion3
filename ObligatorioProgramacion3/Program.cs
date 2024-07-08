using Microsoft.EntityFrameworkCore;
using ObligatorioProgramacion3.Datos;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la conexi�n a SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL"))
);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el pipeline de manejo de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    // Configuraci�n del manejador de excepciones para entornos que no son de desarrollo.
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es 30 d�as. Puede que desees cambiar esto para escenarios de producci�n, ver https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Redirecci�n de HTTP a HTTPS
app.UseHttpsRedirection();
// Habilitar el uso de archivos est�ticos (CSS, JavaScript, im�genes, etc.)
app.UseStaticFiles();

// Configuraci�n del enrutamiento
app.UseRouting();

// Configuraci�n de la autorizaci�n
app.UseAuthorization();

// Definici�n de las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ejecutar la aplicaci�n
app.Run();
