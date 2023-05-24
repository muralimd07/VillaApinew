using System.ComponentModel.DataAnnotations;

namespace VillaApinew.Modal
{
    public class VillaNumberCreateDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string? specialdetials { get; set; }
    }
}
