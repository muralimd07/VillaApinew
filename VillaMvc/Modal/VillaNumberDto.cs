using System.ComponentModel.DataAnnotations;

namespace VillaMvc.Modal
{
    public class VillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string specialdetials { get; set; }


    }
}
