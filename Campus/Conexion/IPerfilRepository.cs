using Campus.Models;

namespace Campus.Conexion
{
    public interface IPerfilRepository
    {
        // Para estudiantes
        IEnumerable<Estudiante> GetEstudiantes();
        void CrearEstudiante(Estudiante est);
        bool ExisteEstudiante(int matricula);

        // Para perfiles
        Perfil GetPerfil(int idPerfil, int matricula);
        IEnumerable<Perfil> GetPerfiles(int matricula);
        void CrearPerfil(int matrivula, Perfil perfil);
        bool Guardar();
    }
}
