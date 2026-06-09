using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoholBusTicketing.Core.Models;

    public class Municipality
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<Barangay> Barangays { get; set; } = new List<Barangay>();
    }

