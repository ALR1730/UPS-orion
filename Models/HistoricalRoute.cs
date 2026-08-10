using System;

namespace OrionMVP.Models
{
    public class HistoricalRoute
    {
        public int Id { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public double AverageDistanceKm { get; set; }
        public int RecordedStopsCount { get; set; }
    }

    public class OdometerLog
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int DriverId { get; set; }
        public int InitialKm { get; set; }
        public int FinalKm { get; set; }
        public int TotalKm => FinalKm - InitialKm;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
