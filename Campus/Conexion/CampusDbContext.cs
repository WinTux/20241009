using Campus.Models;
using Microsoft.EntityFrameworkCore;

namespace Campus.Conexion
{
    public class CampusDbContext : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public CampusDbContext(DbContextOptions<CampusDbContext> op) : base(op)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // estudiante 1 => n perfiles -> perfil 1 : 1 estudiante (FK)
            modelBuilder.Entity<Estudiante>().HasMany(es => es.perfiles)
                .WithOne(p => p.estudiante!).HasForeignKey(p => p.estudianteMatricula);
            // perfil 1 => 1 estudiante -> estudiante 1 : n perfiles
            modelBuilder.Entity<Perfil>().HasOne(p => p.estudiante)
                .WithMany(es => es.perfiles).HasForeignKey(p => p.estudianteMatricula);
        }
    }
}
