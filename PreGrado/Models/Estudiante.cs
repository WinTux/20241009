using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreGrado.Models
{
    [Table(name:"Estudiante", Schema = "pregrado")] 
    public class Estudiante
    {
        [Key]
        [Required]
        public int Matricula { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre {get;set;}
        [Required]
        [MaxLength(100)]
        public string Apellido { get; set;}
        public DateTime? FechaNacimiento {  get; set; }
        public int? CarreraID { get; set; }
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
