using System.ComponentModel.DataAnnotations;

namespace Campus.DTO
{
    public class PerfilCreateDTO
    {
        [Required]
        public string nick { get; set; }
        [Required]
        public string descripcion { get; set; }
        public string lenguajes { get; set; }
    }
}
