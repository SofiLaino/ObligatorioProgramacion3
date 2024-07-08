using Microsoft.EntityFrameworkCore;
using ObligatorioProgramacion3.Models;

namespace ObligatorioProgramacion3.Datos
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuración del contexto
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones) : base(opciones)
        {
        }

        // Definición de DbSet para cada modelo
        public DbSet<Ejercicio> Ejercicios { get; set; }
        public DbSet<Local> Locales { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Rutina> Rutinas { get; set; }
        public DbSet<RutinaSocio> RutinasSocios { get; set; }
        public DbSet<RutinaEjercicio> RutinasEjercicios { get; set; }
        public DbSet<TipoMaquina> TipoMaquinas { get; set; }
        public DbSet<Socio> Socios { get; set; }
        public DbSet<Responsable> Responsables { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<TipoRutina> TipoRutinas { get; set; }
        public DbSet<TipoSocio> TipoSocios { get; set; }
        public DbSet<TipoMaquina> TiposMaquina { get; set; }

        // Configuración adicional del modelo usando Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la entidad RutinaEjercicio con una clave compuesta
            modelBuilder.Entity<RutinaEjercicio>()
                .HasKey(re => new { re.IdRutina, re.IdEjercicio });

            // Configuración de la relación entre Rutina y RutinaEjercicio
            modelBuilder.Entity<RutinaEjercicio>()
                .HasOne(re => re.Rutina)
                .WithMany(r => r.RutinasEjercicios)
                .HasForeignKey(re => re.IdRutina);

            // Configuración de la relación entre Ejercicio y RutinaEjercicio
            modelBuilder.Entity<RutinaEjercicio>()
                .HasOne(re => re.Ejercicio)
                .WithMany(e => e.RutinasEjercicios)
                .HasForeignKey(re => re.IdEjercicio);

            // Configuración de la entidad RutinaSocio con una clave compuesta
            modelBuilder.Entity<RutinaSocio>()
                .HasKey(rs => new { rs.IdRutina, rs.IdSocio });

            // Configuración de la relación entre Rutina y RutinaSocio
            modelBuilder.Entity<RutinaSocio>()
                .HasOne(rs => rs.Rutina)
                .WithMany(r => r.RutinasSocios)
                .HasForeignKey(rs => rs.IdRutina);

            // Configuración de la relación entre Socio y RutinaSocio
            modelBuilder.Entity<RutinaSocio>()
                .HasOne(rs => rs.Socio)
                .WithMany(s => s.RutinasSocios)
                .HasForeignKey(rs => rs.IdSocio);

            // Configuración adicional para la entidad RutinaSocio
            modelBuilder.Entity<RutinaSocio>(entity =>
            {
                entity.HasKey(e => new { e.IdRutina, e.IdSocio });
                entity.Property(e => e.Calificacion).HasColumnType("decimal(18, 2)");
            });

            // Configuración de la relación uno a muchos entre Ciudad y Local
            modelBuilder.Entity<Ciudad>()
                .HasMany(c => c.Locales)
                .WithOne(l => l.Ciudad)
                .HasForeignKey(l => l.CiudadId);

            // Configuración de la relación uno a muchos entre Local y Maquina
            modelBuilder.Entity<Local>()
                .HasMany(l => l.Maquinas)
                .WithOne(m => m.LocalAsociado)
                .HasForeignKey(m => m.LocalAsociadoId);

            // Configuración de la relación uno a muchos entre TipoMaquina y Maquina
            modelBuilder.Entity<Maquina>()
                .HasOne(m => m.TipoMaquina)
                .WithMany()
                .HasForeignKey(m => m.TipoMaquinaId);

            // Insertar ciudades
            modelBuilder.Entity<Ciudad>().HasData(
                new Ciudad { Id = 1, Nombre = "Tarariras" },
                new Ciudad { Id = 2, Nombre = "Colonia del Sacramento" },
                new Ciudad { Id = 3, Nombre = "Juan Lacaze" },
                new Ciudad { Id = 4, Nombre = "Rosario" }
            );

            // Insertar responsables
            modelBuilder.Entity<Responsable>().HasData(
                new Responsable { Id = 1, Nombre = "Agustina", Apellido = "Pellaton", Telefono = "091672542" },
                new Responsable { Id = 2, Nombre = "Juan", Apellido = "Perez", Telefono = "091655818" }
            );

            // Insertar tipos de máquinas
            modelBuilder.Entity<TipoMaquina>().HasData(
                new TipoMaquina { Id = 1, Nombre = "Fuerza" },
                new TipoMaquina { Id = 2, Nombre = "Cardio" }
            );

            // Insertar tipos de socios
            modelBuilder.Entity<TipoSocio>().HasData(
                new TipoSocio { Id = 1, Nombre = "Premium" },
                new TipoSocio { Id = 2, Nombre = "Standard" }
            );

            // Insertar tipos de rutinas
            modelBuilder.Entity<TipoRutina>().HasData(
                new TipoRutina { Id = 1, Nombre = "Bajar de peso" },
                new TipoRutina { Id = 2, Nombre = "Aumentar masa muscular" },
                new TipoRutina { Id = 3, Nombre = "Definicion" }
            );

            // Insertar socios
            modelBuilder.Entity<Socio>().HasData(
                new Socio { Id = 1, Email = "agusPellaton@gmail.com", TipoSocioId = 2, Nombre = "Agustina", Apellido = "Pellaton", Telefono = "091672542", LocalAsociadoId = 1 },
                new Socio { Id = 2, Email = "fioGeymonat@gmail.com", TipoSocioId = 2, Nombre = "Fiorella", Apellido = "Geymonat", Telefono = "098564234", LocalAsociadoId = 1 },
                new Socio { Id = 3, Email = "sofiaLaino@gmail.com", TipoSocioId = 1, Nombre = "Sofia", Apellido = "Laino", Telefono = "099765234", LocalAsociadoId = 2 }
            );

            // Insertar rutinas
            modelBuilder.Entity<Rutina>().HasData(
                new Rutina { Id = 1, Nombre = "Full", TipoRutinaId = 1, Descripcion = "Semanal" },
                new Rutina { Id = 2, Nombre = "Pierna", TipoRutinaId = 2, Descripcion = "Semanal" }
            );

            // Insertar ejercicios
            modelBuilder.Entity<Ejercicio>().HasData(
                new Ejercicio { Id = 1, Nombre = "Caminar", MaquinaId = 2 },
                new Ejercicio { Id = 2, Nombre = "Correr", MaquinaId = 2 }
            );

            // Insertar máquinas
            modelBuilder.Entity<Maquina>().HasData(
                new Maquina { Id = 1, Nombre = "Caminadora", LocalAsociadoId = 2, FechaCompra = new DateTime(2020, 5, 12), PrecioCompra = 12000,VidaUtil = 7,  TipoMaquinaId = 2, Disponible = true },
                new Maquina { Id = 2, Nombre = "Prensa", LocalAsociadoId = 1, FechaCompra = new DateTime(2019, 12, 10), PrecioCompra = 20000, VidaUtil = 10,  TipoMaquinaId = 1, Disponible = false }
            );

            // Insertar locales
            modelBuilder.Entity<Local>().HasData(
                new Local { Id = 1, Nombre = "Mega", CiudadId = 1, Direccion = "La paz 2138", Telefono = "098543124", ResponsableId = 1 },
                new Local { Id = 2, Nombre = "Oxigeno", CiudadId = 2, Direccion = "Mangaeli 1234", Telefono = "091453122", ResponsableId = 2 }
            );


        }
    }
}
