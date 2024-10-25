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
            throw new NotImplementedException();
        }

        public IEnumerable<Perfil> GetPerfiles(int matricula)
        {
            throw new NotImplementedException();
        }
        public void CrearPerfil(int matrivula, Perfil perfil)
        {
            throw new NotImplementedException();
        }

        public bool Guardar()
        {
            throw new NotImplementedException();
        }
    }
}
