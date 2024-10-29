using AutoMapper;
using PreGrado.DTO;
using PreGrado.Models;
namespace PreGrado.DTO_perfiles
{
    public class EstudianteProfile : Profile
    {
        public EstudianteProfile()
        {
            CreateMap<Estudiante,EstudianteReadDTO>(); // --->
            CreateMap<EstudianteCreateDTO, Estudiante>(); // --->
            CreateMap<EstudianteUpdateDTO, Estudiante>(); // --->
            CreateMap<Estudiante, EstudianteUpdateDTO>(); // --->
            CreateMap<EstudianteReadDTO, EstudiantePublisherDTO>(); // --->
        }
    }
}
