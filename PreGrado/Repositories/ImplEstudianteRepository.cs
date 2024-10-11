using PreGrado.Models;

namespace PreGrado.Repositories
{
    public class ImplEstudianteRepository : IEstudianteRepository
    {
        private readonly UniversidadDbContext _context;
        public ImplEstudianteRepository(UniversidadDbContext context)
        {
            _context = context;
        }

        public void AddEstudiante(Estudiante estudiante)
        {
            if (estudiante == null)
                throw new ArgumentNullException(nameof(estudiante));
            _context.Estudiantes.Add(estudiante);
        }

        public Estudiante GetEstudianteByMatricula(int matricula)
        {
            // linQ -> SQL
            return _context.Estudiantes.FirstOrDefault(est => est.Matricula == matricula); // SELECT LIMIT 1 * FROM Estudiante WHERE Matricula = matricula;
        }

        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return _context.Estudiantes.Where(est => est.Estado == true).ToList();// SELECT * FROM Estudiante WHERE Estado = 1;
            //return _context.Estudiantes.ToList(); // SELECT * FROM Estudiante;
        }

        public bool Guardar()
        {
            return (_context.SaveChanges()>-1);
        }
    }
}
