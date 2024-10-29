using System.ComponentModel.DataAnnotations;

namespace Campus.DTO
{
    public class PerfilReadDTO
    {
        public int id { get; set; }
        public string nick { get; set; }
        public string lenguajes { get; set; }
        public int estudianteMatricula { get; set; }
    }
}
