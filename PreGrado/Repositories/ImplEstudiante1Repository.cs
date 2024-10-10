using PreGrado.Models;

namespace PreGrado.Repositories
{
    public class ImplEstudiante1Repository : IEstudiante1Repository
    {
        public Estudiante1 GetEstudianteById(int id)
        {
            return new Estudiante1
            {
                id = 100,
                nombre = "Pepe Perales",
                carrera = "Contabilidad",
                email = "pepe@gmail.com"
            };
        }

        public IEnumerable<Estudiante1> GetEstudiantes()
        {
            var estudiantes = new List<Estudiante1> { 
                new Estudiante1{ 
                    id = 1,
                    nombre = "Pepe Perales",
                    carrera = "Contabilidad",
                    email = "pepe@gmail.com"
                },
                new Estudiante1{
                    id = 2,
                    nombre = "ana Sosa",
                    carrera = "Física",
                    email = "anita@gmail.com"
                },
                new Estudiante1{
                    id = 3,
                    nombre = "sofía Rocha",
                    carrera = "Contabilidad",
                    email = "sofisofi@outlook.com"
                },
                new Estudiante1{
                    id = 4,
                    nombre = "Samantha Barrios",
                    carrera = "Filosofía",
                    email = "sam@gmail.com"
                }
            };
            return estudiantes;
        }
    }
}
