using System.ComponentModel.DataAnnotations;

namespace PreGrado.DTO
{
    public class EstudianteUpdateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; }
        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
