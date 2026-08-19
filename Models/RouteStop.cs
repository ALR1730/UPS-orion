using System;
using System.Globalization;

namespace OrionMVP.Models
{
    public class RouteStop
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public Route? Route { get; set; }

        public int SequenceOrder { get; set; }
        public int Sequence { get => SequenceOrder; set => SequenceOrder = value; }
        
        public string ArticleName { get; set; } = "Paquete Estándar";
        public string CustomerName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty; // Altura
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        
        public string FullAddress
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Address)) return Address;
                if (!string.IsNullOrWhiteSpace(Street) || !string.IsNullOrWhiteSpace(City))
                    return $"{Street} {Number}, {City}".Trim().Trim(',').Trim();
                return "Ubicación asignada";
            }
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public bool IsGeocoded { get; set; } = true;
        public bool HasGeocodingError { get; set; } = false;

        public string Status { get; set; } = "Pendiente"; // "Pendiente", "Entregado", "No entregado"
        public string? CancellationReason { get; set; }

        public string ExternalNavUrl { get; set; } = string.Empty;

        // External Navigation URL Generators (HU05)
        public string GetGoogleMapsUrl()
        {
            if (Latitude != 0 && Longitude != 0)
            {
                return $"https://www.google.com/maps/search/?api=1&query={Latitude.ToString(CultureInfo.InvariantCulture)},{Longitude.ToString(CultureInfo.InvariantCulture)}";
            }
            var encodedAddress = Uri.EscapeDataString(FullAddress);
            return $"https://www.google.com/maps/search/?api=1&query={encodedAddress}";
        }
    }
}
