using AutoMapper;
using Campus.Conexion;
using Campus.DTO;
using Campus.Models;
using Microsoft.AspNetCore.Mvc;

namespace Campus.Controllers
{
    [Route("api/c/perfil/{matricula}")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilRepository repositorio;
        private readonly IMapper mapper;
        public PerfilController(IPerfilRepository repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PerfilReadDTO>> GetPerfilesDeEstudiante(int matricula)
        {
            Console.WriteLine($"Se obtienen perfiles de estudiante con matricula {matricula}");
            var perfiles = repositorio.GetPerfiles(matricula);
            return Ok(mapper.Map<IEnumerable<PerfilReadDTO>>(perfiles));
        }
        [HttpGet("{perfilid}", Name = "GetPerfilDeEstudiante")]
        public ActionResult<IEnumerable<PerfilReadDTO>> GetPerfilDeEstudiante(int matricula, int perfilid)
        {
            Console.WriteLine($"Se obtiene perfil {perfilid} de estudiante con matricula {matricula}");
            if (!repositorio.ExisteEstudiante(matricula))
                return NotFound();
            var perfil = repositorio.GetPerfil(perfilid, matricula);
            if(perfil == null)
                return NotFound();
            return Ok(mapper.Map<PerfilReadDTO>(perfil));
        }
        [HttpPost]
        public ActionResult<PerfilReadDTO> CrearPerfilParaEstudiante(int matricula, PerfilCreateDTO perfilDTO) {
            Console.WriteLine($"Se creara perfil para estudiante con matricula {matricula}");
            if(!repositorio.ExisteEstudiante(matricula))
                return NotFound();
            Perfil perfil = mapper.Map<Perfil>(perfilDTO);
            repositorio.CrearPerfil(matricula, perfil);
            repositorio.Guardar();
            var perfilReadDTO = mapper.Map<PerfilReadDTO>(perfil);
            return CreatedAtRoute(nameof(GetPerfilDeEstudiante), new { matricula = matricula, perfilid = perfilReadDTO.id},perfilReadDTO);
        }
    }
}
