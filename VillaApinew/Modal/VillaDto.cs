using System.ComponentModel.DataAnnotations;

namespace VillaApinew.Modal
{
    public class VillaDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
