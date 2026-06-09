using System;
using System.Collections.Generic;
using BoholBusTicketing.Core.Models;

namespace BoholBusTicketing.Core.Services;


    internal static class DistanceCalculator
    {
        private static readonly Dictionary<string, MunicipalityCoordinate> MunicipalityCoordinates =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["tagbilaran city"] = new MunicipalityCoordinate { Name = "Tagbilaran City", Latitude = 9.6571, Longitude = 123.8520 },
                ["dauis"] = new MunicipalityCoordinate { Name = "Dauis", Latitude = 9.6400, Longitude = 123.7860 },
                ["baclayon"] = new MunicipalityCoordinate { Name = "Baclayon", Latitude = 9.6570, Longitude = 123.9220 },
                ["corella"] = new MunicipalityCoordinate { Name = "Corella", Latitude = 9.7360, Longitude = 123.7750 },
                ["cortes"] = new MunicipalityCoordinate { Name = "Cortes", Latitude = 9.6850, Longitude = 123.7850 },
                ["panglao"] = new MunicipalityCoordinate { Name = "Panglao", Latitude = 9.6200, Longitude = 123.8110 },
                ["alburquerque"] = new MunicipalityCoordinate { Name = "Alburquerque", Latitude = 9.7410, Longitude = 123.7670 },
                ["loay"] = new MunicipalityCoordinate { Name = "Loay", Latitude = 9.6130, Longitude = 123.9500 },
                ["maribojoc"] = new MunicipalityCoordinate { Name = "Maribojoc", Latitude = 9.6540, Longitude = 123.8700 },
                ["sikatuna"] = new MunicipalityCoordinate { Name = "Sikatuna", Latitude = 9.7930, Longitude = 123.9500 },
                ["loboc"] = new MunicipalityCoordinate { Name = "Loboc", Latitude = 9.7230, Longitude = 123.9250 },
                ["antequera"] = new MunicipalityCoordinate { Name = "Antequera", Latitude = 9.8560, Longitude = 123.8220 },
                ["balilihan"] = new MunicipalityCoordinate { Name = "Balilihan", Latitude = 9.7700, Longitude = 123.8800 },
                ["lila"] = new MunicipalityCoordinate { Name = "Lila", Latitude = 9.6150, Longitude = 123.8500 },
                ["dimiao"] = new MunicipalityCoordinate { Name = "Dimiao", Latitude = 9.7190, Longitude = 123.9650 },
                ["valencia"] = new MunicipalityCoordinate { Name = "Valencia", Latitude = 9.8150, Longitude = 124.1270 },
                ["sevilla"] = new MunicipalityCoordinate { Name = "Sevilla", Latitude = 9.8150, Longitude = 123.9700 },
                ["bilar"] = new MunicipalityCoordinate { Name = "Bilar", Latitude = 9.8360, Longitude = 124.0410 },
                ["carmen"] = new MunicipalityCoordinate { Name = "Carmen", Latitude = 9.8200, Longitude = 123.9970 },
                ["batuan"] = new MunicipalityCoordinate { Name = "Batuan", Latitude = 9.7970, Longitude = 123.9570 },
                ["catigbian"] = new MunicipalityCoordinate { Name = "Catigbian", Latitude = 9.7280, Longitude = 123.9510 },
                ["sagbayan"] = new MunicipalityCoordinate { Name = "Sagbayan", Latitude = 9.9200, Longitude = 124.0240 },
                ["tubigon"] = new MunicipalityCoordinate { Name = "Tubigon", Latitude = 9.9360, Longitude = 123.8840 },
                ["calape"] = new MunicipalityCoordinate { Name = "Calape", Latitude = 9.8670, Longitude = 123.9300 },
                ["loon"] = new MunicipalityCoordinate { Name = "Loon", Latitude = 9.9130, Longitude = 123.9100 },
                ["clarin"] = new MunicipalityCoordinate { Name = "Clarin", Latitude = 10.0040, Longitude = 123.8160 },
                ["inabanga"] = new MunicipalityCoordinate { Name = "Inabanga", Latitude = 10.1150, Longitude = 124.0410 },
                ["buenavista"] = new MunicipalityCoordinate { Name = "Buenavista", Latitude = 10.1350, Longitude = 124.1310 },
                ["getafe"] = new MunicipalityCoordinate { Name = "Getafe", Latitude = 9.7910, Longitude = 123.9930 },
                ["talibon"] = new MunicipalityCoordinate { Name = "Talibon", Latitude = 10.1730, Longitude = 124.2870 },
                ["trinidad"] = new MunicipalityCoordinate { Name = "Trinidad", Latitude = 10.0430, Longitude = 124.3340 },
                ["san isidro"] = new MunicipalityCoordinate { Name = "San Isidro", Latitude = 9.9850, Longitude = 124.2860 },
                ["danao"] = new MunicipalityCoordinate { Name = "Danao", Latitude = 9.9280, Longitude = 124.2630 },
                ["dagohoy"] = new MunicipalityCoordinate { Name = "Dagohoy", Latitude = 10.1030, Longitude = 124.1650 },
                ["pilar"] = new MunicipalityCoordinate { Name = "Pilar", Latitude = 9.8700, Longitude = 124.2380 },
                ["sierra bullones"] = new MunicipalityCoordinate { Name = "Sierra Bullones", Latitude = 9.9370, Longitude = 124.0550 },
                ["alicia"] = new MunicipalityCoordinate { Name = "Alicia", Latitude = 9.8850, Longitude = 124.1840 },
                ["candijay"] = new MunicipalityCoordinate { Name = "Candijay", Latitude = 9.5140, Longitude = 124.3840 },
                ["anda"] = new MunicipalityCoordinate { Name = "Anda", Latitude = 9.4650, Longitude = 124.4530 },
                ["guindulman"] = new MunicipalityCoordinate { Name = "Guindulman", Latitude = 9.7070, Longitude = 124.2640 },
                ["duero"] = new MunicipalityCoordinate { Name = "Duero", Latitude = 9.6010, Longitude = 124.2250 },
                ["jagna"] = new MunicipalityCoordinate { Name = "Jagna", Latitude = 9.5260, Longitude = 124.2630 },
                ["garcia hernandez"] = new MunicipalityCoordinate { Name = "Garcia Hernandez", Latitude = 9.5480, Longitude = 124.2770 },
                ["mabini"] = new MunicipalityCoordinate { Name = "Mabini", Latitude = 9.6010, Longitude = 124.0420 },
                ["ubay"] = new MunicipalityCoordinate { Name = "Ubay", Latitude = 10.0850, Longitude = 124.3640 },
                ["san miguel"] = new MunicipalityCoordinate { Name = "San Miguel", Latitude = 9.9470, Longitude = 124.3140 },
                ["president carlos p. garcia"] = new MunicipalityCoordinate { Name = "President Carlos P. Garcia", Latitude = 9.7970, Longitude = 124.3260 },
                ["bien unido"] = new MunicipalityCoordinate { Name = "Bien Unido", Latitude = 10.1500, Longitude = 124.4560 }
            };

        public static bool TryResolveMunicipalityCoordinate(Municipality municipality, out (double Latitude, double Longitude) coordinates)
        {
            coordinates = default;
            if (municipality == null || string.IsNullOrWhiteSpace(municipality.Name))
                return false;

            if (municipality.Latitude != 0 || municipality.Longitude != 0)
            {
                coordinates = (municipality.Latitude, municipality.Longitude);
                return true;
            }

            var normalized = NormalizeMunicipalityName(municipality.Name);
            if (MunicipalityCoordinates.TryGetValue(normalized, out var coordinate))
            {
                coordinates = (coordinate.Latitude, coordinate.Longitude);
                return true;
            }

            return false;
        }

        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double earthRadiusKm = 6371.0;
            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a = Math.Pow(Math.Sin(dLat / 2), 2) +
                    Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                    Math.Pow(Math.Sin(dLon / 2), 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return earthRadiusKm * c;
        }

        private static double DegreesToRadians(double degrees) => degrees * (Math.PI / 180);

        private static string NormalizeMunicipalityName(string name)
        {
            var normalized = name.Trim().ToLowerInvariant();
            return normalized switch
            {
                "tagbilaran" => "tagbilaran city",
                "pres. carlos p. garcia" => "president carlos p. garcia",
                _ => normalized,
            };
        }
    }

