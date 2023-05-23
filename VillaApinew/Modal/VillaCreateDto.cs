using System.ComponentModel.DataAnnotations;

namespace VillaApinew.Modal
{
    public class VillaCreateDto
    {
       
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Details { get; set; }
        public double Rate { get; set; }
        [Required]
        public double Sqrt { get; set; }
        [Required]
        public int Occupancy { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
    }
}
