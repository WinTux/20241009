using AutoMapper;
using Campus.Conexion;
using Campus.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Campus.Controllers
{
    [Route("api/c/estudiante")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IPerfilRepository repositorio;
        private readonly IMapper mapper;
        public EstudianteController(IPerfilRepository repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<EstudianteReadDTO>> GetEstudiantes()
        {
            Console.WriteLine("Se obtiene estudiantes (mediante Servicio Campus)");
            var estudiantes = repositorio.GetEstudiantes();
            return Ok(mapper.Map<IEnumerable<EstudianteReadDTO>>(estudiantes));
        }
    }
}
