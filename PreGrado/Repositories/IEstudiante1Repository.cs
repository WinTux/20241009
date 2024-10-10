using PreGrado.Models;

namespace PreGrado.Repositories
{
    public interface IEstudiante1Repository
    {
        IEnumerable<Estudiante1> GetEstudiantes();
        Estudiante1 GetEstudianteById(int id);
    }
}
