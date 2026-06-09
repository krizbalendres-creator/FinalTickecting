using System.ComponentModel.DataAnnotations;

namespace BoholBusTicketing.Core.DTOs
{
    public class ConductorViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a route preset.")]
        [Display(Name = "Route Preset")]
        public int RoutePresetId { get; set; }

        public string RoutePresetLabel { get; set; } = string.Empty;
    }
}
