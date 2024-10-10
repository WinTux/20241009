using Microsoft.EntityFrameworkCore;
using PreGrado.Models;

namespace PreGrado.Repositories
{
    public class UniversidadDbContext : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public UniversidadDbContext(DbContextOptions<UniversidadDbContext> op) : base(op) {
            
        }
    }
}
