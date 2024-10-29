using PreGrado.DTO;

namespace PreGrado.ComunicacionAsync
{
    public interface IBusDeMensajesCliente
    {
        void PublicarNuevoEstudiante(EstudianteReadDTO estudianteReadDTO);
    }
}
