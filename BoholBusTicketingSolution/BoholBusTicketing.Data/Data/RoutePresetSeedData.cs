using BoholBusTicketing.Core.DTOs;

namespace BoholBusTicketing.Data.Data
{
    public static class RoutePresetSeedData
    {
        public static List<RoutePresetDto> GetRouteDefinitions()
        {
            return new List<RoutePresetDto>
            {
                new RoutePresetDto
                {
                    Label = "Panglao, Bohol → Tagbilaran City, Bohol",
                    FromMunicipality = "Panglao",
                    ToMunicipality = "Tagbilaran City",
                    RouteMunicipalities = new List<string> { "Panglao", "Dauis", "Tagbilaran City" }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Pitogo, Bohol",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Pitogo",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Cortes",
                        "Balilihan",
                        "Batuan",
                        "Carmen",
                        "Pilar",
                        "Alicia",
                        "Ubay",
                        "Pitogo"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Anda, Bohol",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Anda",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Baclayon",
                        "Alburquerque",
                        "Loay",
                        "Lila",
                        "Garcia Hernandez",
                        "Jagna",
                        "Duero",
                        "Guindulman",
                        "Anda"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Mabini, Bohol",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Mabini",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Cortes",
                        "Balilihan",
                        "Batuan",
                        "Carmen",
                        "Pilar",
                        "Alicia",
                        "Mabini"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Bien Unido, Bohol",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Bien Unido",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Cortes",
                        "Balilihan",
                        "Batuan",
                        "Carmen",
                        "Dagohoy",
                        "San Miguel",
                        "Trinidad",
                        "Bien Unido"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Guindulman, Bohol",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Guindulman",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Baclayon",
                        "Alburquerque",
                        "Loay",
                        "Lila",
                        "Garcia Hernandez",
                        "Jagna",
                        "Duero",
                        "Guindulman"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Talibon, Bohol (via Antequera)",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Talibon",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Cortes",
                        "Antequera",
                        "Balilihan",
                        "Catigbian",
                        "Batuan",
                        "Carmen",
                        "San Miguel",
                        "Trinidad",
                        "Talibon"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Talibon, Bohol (via Carmen)",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Talibon",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Cortes",
                        "Balilihan",
                        "Catigbian",
                        "Batuan",
                        "Carmen",
                        "Loay",
                        "Ubay",
                        "Talibon"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Talibon, Bohol (via Tubigon)",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Talibon",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Cortes",
                        "Maribojoc",
                        "Loon",
                        "Calape",
                        "Tubigon",
                        "Clarin",
                        "Inabanga",
                        "Buenavista",
                        "Getafe",
                        "Trinidad",
                        "Talibon"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Talibon, Bohol (via Ubay/Jagna)",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Talibon",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Baclayon",
                        "Alburquerque",
                        "Loay",
                        "Lila",
                        "Garcia Hernandez",
                        "Jagna",
                        "Duero",
                        "Guindulman",
                        "Candijay",
                        "Alicia",
                        "Ubay",
                        "Trinidad",
                        "Talibon"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Tubigon, Bohol",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Tubigon",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Cortes",
                        "Maribojoc",
                        "Loon",
                        "Calape",
                        "Tubigon"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Tubigon, Bohol (via Antequera)",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Tubigon",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Cortes",
                        "Antequera",
                        "Balilihan",
                        "Catigbian",
                        "Tubigon"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Sagbayan, Bohol (via Tumoc)",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Sagbayan",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Cortes",
                        "Maribojoc",
                        "Loon",
                        "Calape",
                        "Tubigon",
                        "Clarin",
                        "Sagbayan"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Ubay, Bohol (via Balilihan/Carmen)",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Ubay",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Cortes",
                        "Antequera",
                        "Balilihan",
                        "Catigbian",
                        "Batuan",
                        "Carmen",
                        "San Miguel",
                        "Trinidad",
                        "Ubay"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Ubay, Bohol (via Mabini)",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Ubay",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Baclayon",
                        "Alburquerque",
                        "Loay",
                        "Lila",
                        "Garcia Hernandez",
                        "Jagna",
                        "Duero",
                        "Guindulman",
                        "Candijay",
                        "Alicia",
                        "Mabini",
                        "Ubay"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Ubay, Bohol (via Jagna)",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Ubay",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Baclayon",
                        "Alburquerque",
                        "Loay",
                        "Lila",
                        "Garcia Hernandez",
                        "Jagna",
                        "Duero",
                        "Guindulman",
                        "Candijay",
                        "Alicia",
                        "Mabini",
                        "Ubay"
                    }
                },
                new RoutePresetDto
                {
                    Label = "Tagbilaran City, Bohol → Ubay, Bohol (via Sierra Bullones/Carmen)",
                    FromMunicipality = "Tagbilaran City",
                    ToMunicipality = "Ubay",
                    RouteMunicipalities = new List<string>
                    {
                        "Tagbilaran City",
                        "Cortes",
                        "Antequera",
                        "Balilihan",
                        "Carmen",
                        "Sierra Bullones",
                        "Pilar",
                        "Alicia",
                        "Ubay"
                    }
                }
            };
        }
    }
}
