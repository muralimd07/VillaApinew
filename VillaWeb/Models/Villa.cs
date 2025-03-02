﻿using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaWeb.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public double Rate { get; set; }

        public double Sqrt { get; set; }

        public int Occupancy { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }


    }
}
