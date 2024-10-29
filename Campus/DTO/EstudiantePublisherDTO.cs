namespace Campus.DTO
{
    public class EstudiantePublisherDTO
    {
        public int Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string tipoEvento { get; set; }// extra e informativo
    }
}
