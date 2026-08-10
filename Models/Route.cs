using System;
using System.Collections.Generic;

namespace OrionMVP.Models
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public int DriverId { get; set; }
        public Driver? Driver { get; set; }

        public string Status { get; set; } = "No iniciado"; // "No iniciado", "En Ruta", "Finalizado"

        public int? InitialKm { get; set; }
        public int? FinalKm { get; set; }
        
        public double TotalDistanceKm { get; set; } = 0.0;
        public double BaselineDistanceKm { get; set; } = 0.0; // Promedio histórico

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<RouteStop> Stops { get; set; } = new();
    }
}
