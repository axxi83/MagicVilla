using MagicVilla_API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa() 
                { 
                    Id = 1,
                    Nombre = "Villa Real",
                    Detalle = "Detalle de la villa",
                    ImagenUrl = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 50,
                    Tarifa = 200,
                    Amenidad = "",
                    FechaActualizacion = DateTime.Now,
                    FechaCreacion = DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Nombre = "Villa Brasil",
                    Detalle = "Detalle de la villa",
                    ImagenUrl = "",
                    Ocupantes = 4,
                    MetrosCuadrados = 60,
                    Tarifa = 250,
                    Amenidad = "",
                    FechaActualizacion = DateTime.Now,
                    FechaCreacion = DateTime.Now
                }
            );
        }
    }
}
