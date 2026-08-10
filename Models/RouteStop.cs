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
    }
}
