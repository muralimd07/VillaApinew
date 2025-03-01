using System.ComponentModel.DataAnnotations;

namespace VillaWeb.Models
{
    public class VillaDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Details { get; set; }
        public double Rate { get; set; }

        public double Sqrt { get; set; }

        public int Occupancy { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
    }
}
