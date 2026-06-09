using System.ComponentModel.DataAnnotations;

namespace BoholBusTicketing.Core.DTOs
{
    public class RoutePresetViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Label { get; set; } = string.Empty;

        [Required]
        public string FromMunicipality { get; set; } = string.Empty;

        [Required]
        public string ToMunicipality { get; set; } = string.Empty;

        [Required]
        public string RouteMunicipalities { get; set; } = string.Empty;
    }
}
