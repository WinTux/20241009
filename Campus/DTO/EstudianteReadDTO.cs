namespace Campus.DTO
{
    public class EstudianteReadDTO
    {
        public int Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Email { get; set; }
    }
}
