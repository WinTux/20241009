using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PreGrado.ComunicacionAsync;
using PreGrado.ComunicacionSync.http;
using PreGrado.DTO;
using PreGrado.Models;
using PreGrado.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PreGrado.Controllers
{
    [Route("api/estudiante")] // http://localhost:7654/api/estudiante
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository estRepo;
        private readonly IMapper mapper;

        public readonly ICampusHistorialCliente campusHistorialCliente;
        private readonly IBusDeMensajesCliente busDeMensajesCliente;

        public EstudianteController(IEstudianteRepository estRepo, IMapper mapper, ICampusHistorialCliente campusHistorialCliente, IBusDeMensajesCliente busDeMensajesCliente)
        {
            this.estRepo = estRepo;
            this.mapper = mapper;
            this.campusHistorialCliente = campusHistorialCliente;
            this.busDeMensajesCliente = busDeMensajesCliente;
        }
        [HttpGet] // http://localhost:7654/api/estudiante [GET]
        public ActionResult<IEnumerable<EstudianteReadDTO>> getEstudiantes()
        {
            var ests = estRepo.GetEstudiantes();
            return Ok(mapper.Map<IEnumerable<EstudianteReadDTO>>(ests));
        }
        [HttpGet("{matricula}", Name = "getEstudianteByMatricula")] // http://localhost:7654/api/estudiante/{matricula} [GET]
        public ActionResult<EstudianteReadDTO> getEstudianteByMatricula(int matricula)
        {
            Estudiante est = estRepo.GetEstudianteByMatricula(matricula);
            if(est != null)
                return Ok(mapper.Map<EstudianteReadDTO>(est));
            return NotFound(); // 404
        }
        [HttpPost]
        public async Task<ActionResult<EstudianteReadDTO>> setEstudiante(EstudianteCreateDTO estCreateDTO) {
            Estudiante estudiante = mapper.Map<Estudiante>(estCreateDTO);
            estudiante.Estado = true;
            estRepo.AddEstudiante(estudiante);
            estRepo.Guardar();
            EstudianteReadDTO estRetorno = mapper.Map<EstudianteReadDTO>(estudiante);

            // Ejemplo de mensaje sincrónico
            try
            {
                await campusHistorialCliente.ComunicarseConCampus(estRetorno);
            }
            catch (Exception e){
                Console.WriteLine($"Ocurrió un error al comunicarse con Campus de forma sincronizada: {e.Message}");
            }
            // Ejemplo de mensaje asíncrono
            try{
                var estudianteDTO = mapper.Map<EstudiantePublisherDTO>(estRetorno);
                estudianteDTO.tipoEvento = "estudiante_publicado";
                busDeMensajesCliente.PublicarNuevoEstudiante(estudianteDTO);
            }catch(Exception e){
                Console.WriteLine($"Ocurrió un error al tratar de publicar: { e.Message}");
            }

            return CreatedAtRoute(nameof(getEstudianteByMatricula), new { matricula = estRetorno.Matricula}, estRetorno ); // 201 Created // location: http://localhost:7654/api/estudiante/123
            
        }
        [HttpPut("{matricula}")] // https://localhost:7654/api/estudiante/{matricula} [PUT]
        public ActionResult updateEstudiante(int matricula, EstudianteUpdateDTO estUpdateDTO) {
            Estudiante estudiante = estRepo.GetEstudianteByMatricula(matricula);
            if (estudiante == null)
                return NotFound(); // 404
            mapper.Map(estUpdateDTO,estudiante);
            estRepo.Guardar();
            return NoContent(); // 204
        }
        [HttpPatch("{matricula}")] // https://localhost:7654/api/estudiante/{matricula} [PUT]
        public ActionResult updateEstudiantePatch(int matricula, JsonPatchDocument<EstudianteUpdateDTO> estPatch)
        {
            Estudiante estudiante = estRepo.GetEstudianteByMatricula(matricula);
            if (estudiante == null)
                return NotFound(); // 404
            EstudianteUpdateDTO estParaPatch = mapper.Map<EstudianteUpdateDTO>(estudiante);
            estPatch.ApplyTo(estParaPatch, ModelState);
            if(!TryValidateModel(estParaPatch))
                return ValidationProblem(ModelState);
            mapper.Map(estParaPatch, estudiante);
            estRepo.UpdateEstudiante(estudiante);
            estRepo.Guardar();
            return NoContent(); // 204
        }
        [HttpDelete("{matricula}")] // https://localhost:7654/api/estudiante/{matricula} [DELETE]
        public ActionResult deleteEstudiante(int matricula) {
            Estudiante estudiante = estRepo.GetEstudianteByMatricula(matricula);
            if (estudiante == null)
                return NotFound(); // 404
            estRepo.DeleteEstudiante(estudiante);
            estRepo.Guardar();
            return NoContent(); // 204
        }
    }
}
