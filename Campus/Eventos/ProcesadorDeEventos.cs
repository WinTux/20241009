using AutoMapper;
using Campus.Conexion;
using Campus.DTO;
using Campus.Models;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Campus.Eventos
{
    public class ProcesadorDeEventos : IProcesadorDeEventos
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IMapper mapper;
        public ProcesadorDeEventos(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.mapper = mapper;
        }
        public void ProcesarEvento(string msj)
        {
            var tipo = DeterminarEvento(msj);//definimos el método más adelante
            switch (tipo)
            {
                case TipoDeEvento.estudiante_publicado:
                    agregarEstudiante(msj);
                    break;
                case TipoDeEvento.desconocido:
                    break;
            }
        }
        private TipoDeEvento DeterminarEvento(string mensaje)
        {
            EventoDTO tipo = JsonSerializer.Deserialize<EventoDTO>(mensaje);
            switch (tipo.evento)
            {
                case "estudiante_publicado":
                    return TipoDeEvento.estudiante_publicado;
                default:
                    return TipoDeEvento.desconocido;
            }
        }
        private void agregarEstudiante(string mensajeEstudiantePublisher)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IPerfilRepository>();
                var estudiantePublisherDTO =
                JsonSerializer.Deserialize<EstudiantePublisherDTO>(mensajeEstudiantePublisher);
                try
                {
                    var est =
                    mapper.Map<Estudiante>(estudiantePublisherDTO);
                    if (!repo.ExisteEstudianteForaneo(est.fMatricula))
                    {
                        repo.CrearEstudiante(est);
                        repo.Guardar();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error al agregar estudiante a la ddbb: { e.Message}");
                }
            }
        }
    }
    enum TipoDeEvento
    {
        estudiante_publicado,
        desconocido
    }
}
