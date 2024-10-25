using System.ComponentModel.DataAnnotations;

namespace Campus.Models
{
    public class Estudiante
    {
        [Key]
        [Required]
        public int Matricula { get; set; }
        public int fMatricula { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? CarreraID { get; set; }
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Perfil> perfiles { get; set; } = new List<Perfil>();
    }
}
