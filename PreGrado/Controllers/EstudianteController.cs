using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PreGrado.DTO;
using PreGrado.Models;
using PreGrado.Repositories;

namespace PreGrado.Controllers
{
    [Route("api/estudiante")] // http://localhost:7654/api/estudiante
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository estRepo;
        private readonly IMapper mapper;
        public EstudianteController(IEstudianteRepository estRepo, IMapper mapper)
        {
            this.estRepo = estRepo;
            this.mapper = mapper;
        }
        [HttpGet] // http://localhost:7654/api/estudiante [GET]
        public ActionResult<IEnumerable<EstudianteReadDTO>> getEstudiantes()
        {
            var ests = estRepo.GetEstudiantes();
            return Ok(mapper.Map<IEnumerable<EstudianteReadDTO>>(ests));
        }
        [HttpGet("{matricula}")] // http://localhost:7654/api/estudiante/{matricula} [GET]
        public ActionResult<EstudianteReadDTO> getEstudianteByMatricula(int matricula)
        {
            Estudiante est = estRepo.GetEstudianteByMatricula(matricula);
            if(est != null)
                return Ok(mapper.Map<EstudianteReadDTO>(est));
            return NotFound(); // 404
        }
    }
}
