using PreGrado.DTO;

namespace PreGrado.ComunicacionSync.http
{
    public interface ICampusHistorialCliente
    {
        Task ComunicarseConCampus(EstudianteReadDTO est);
    }
}
