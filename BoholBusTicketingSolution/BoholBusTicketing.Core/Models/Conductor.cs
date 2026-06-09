using System.ComponentModel.DataAnnotations.Schema;

namespace BoholBusTicketing.Core.Models;

    public class Conductor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }

        public int FromMunicipalityId { get; set; }
        public Municipality? FromMunicipality { get; set; }

        public int ToMunicipalityId { get; set; }
        public Municipality? ToMunicipality { get; set; }

        public int? RoutePresetId { get; set; }
        public RoutePreset? RoutePreset { get; set; }
    }

