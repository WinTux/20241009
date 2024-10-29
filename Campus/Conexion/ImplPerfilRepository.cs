using Campus.Models;

namespace Campus.Conexion
{
    public class ImplPerfilRepository : IPerfilRepository
    {
        private readonly CampusDbContext contexto;
        public ImplPerfilRepository(CampusDbContext contexto)
        {
            this.contexto = contexto;
        }
        // Para estudiantes
        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return contexto.Estudiantes.ToList();
        }
        public void CrearEstudiante(Estudiante est)
        {
            if (est == null)
                throw new ArgumentNullException(nameof(est));
            else
                contexto.Estudiantes.Add(est);
        }
        public bool ExisteEstudiante(int matricula)
        {
            return contexto.Estudiantes.Any(est => est.Matricula == matricula);
        }

        // Para perfiles
        public Perfil GetPerfil(int idPerfil, int matricula)
        {
            return contexto.Perfiles.Where(per=>per.id==idPerfil && per.estudianteMatricula == matricula).FirstOrDefault();
        }

        public IEnumerable<Perfil> GetPerfiles(int matricula)
        {
            return contexto.Perfiles.Where(p => p.estudianteMatricula == matricula).OrderBy(p => p.estudiante.Apellido);
        }
        public void CrearPerfil(int matricula, Perfil perfil)
        {
            if (perfil == null)
                throw new ArgumentNullException(nameof(perfil));
            else {
                perfil.estudianteMatricula = matricula;
                contexto.Perfiles.Add(perfil);
            }

        }

        public bool Guardar()
        {
            return (contexto.SaveChanges() >= 0);
        }

        public bool ExisteEstudianteForaneo(int fmatricula)
        {
            return contexto.Estudiantes.Any(es => es.fMatricula == fmatricula);
        }
    }
}
