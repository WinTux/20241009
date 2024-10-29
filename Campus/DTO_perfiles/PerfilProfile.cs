using AutoMapper;
using Campus.DTO;
using Campus.Models;

namespace Campus.DTO_perfiles
{
    public class PerfilProfile : Profile
    {
        public PerfilProfile()
        {
            CreateMap<Estudiante, EstudianteReadDTO>();

            CreateMap<Perfil, PerfilReadDTO>();

            CreateMap<PerfilCreateDTO, Perfil>();
        }
    }
}
