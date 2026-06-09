namespace BoholBusTicketing.Core.DTOs
{
    public class RoutePresetDto
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string FromMunicipality { get; set; } = string.Empty;
        public string ToMunicipality { get; set; } = string.Empty;
        public List<string> RouteMunicipalities { get; set; } = new();
    }
}
