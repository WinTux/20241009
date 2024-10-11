﻿using PreGrado.Models;

namespace PreGrado.Repositories
{
    public interface IEstudianteRepository
    {
        IEnumerable<Estudiante> GetEstudiantes();
        Estudiante GetEstudianteByMatricula(int matricula);
        void AddEstudiante(Estudiante estudiante);
        bool Guardar();
    }
}
