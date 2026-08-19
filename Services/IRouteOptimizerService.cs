using OrionMVP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrionMVP.Services
{
    public class OptimizationResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public double TotalDistanceKm { get; set; }
        public long ExecutionTimeMs { get; set; }
        public int ProcessedStopsCount { get; set; }
        public List<RouteStop> OptimizedStops { get; set; } = new();
    }

    public interface IRouteOptimizerService
    {
        Task<OptimizationResultDto> OptimizeRouteAsync(int routeId, double startLat = 18.4861, double startLng = -69.9312);
    }
}
