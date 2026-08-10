using System;

namespace OrionMVP.Models
{
    public class RouteStop
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public Route? Route { get; set; }

        public int SequenceOrder { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty; // Altura
        public string City { get; set; } = string.Empty;
        
        public string FullAddress => $"{Street} {Number}, {City}";

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public bool IsGeocoded { get; set; } = false;
        public bool HasGeocodingError { get; set; } = false;

        public string Status { get; set; } = "Pendiente"; // "Pendiente", "Entregado", "No entregado"
        public string? CancellationReason { get; set; }

        public string ExternalNavUrl { get; set; } = string.Empty;

        // External Navigation URL Generators (HU05)
        public string GetGoogleMapsUrl()
        {
            if (Latitude != 0 && Longitude != 0)
            {
                return $"https://www.google.com/maps/search/?api=1&query={Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)},{Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
            }
            var encodedAddress = Uri.EscapeDataString(FullAddress);
            return $"https://www.google.com/maps/search/?api=1&query={encodedAddress}";
        }

        public string GetWazeUrl()
        {
            if (Latitude != 0 && Longitude != 0)
            {
                return $"https://waze.com/ul?ll={Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)},{Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}&navigate=yes";
            }
            var encodedAddress = Uri.EscapeDataString(FullAddress);
            return $"https://waze.com/ul?q={encodedAddress}&navigate=yes";
        }
    }
}
