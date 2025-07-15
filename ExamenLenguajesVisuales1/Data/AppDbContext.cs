using ExamenLenguajesVisuales1.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenLenguajesVisuales1.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Cambia los valores según tu entorno
            optionsBuilder.UseSqlServer("Server=CompudeMati\\SQLEXPRESS;Database=BasePrueba;User Id=mati;Password=123456789;TrustServerCertificate=True;");
        }
    }
}