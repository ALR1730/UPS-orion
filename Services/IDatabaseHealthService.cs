using System.Threading.Tasks;

namespace OrionMVP.Services
{
    public class DatabaseHealthDto
    {
        public bool IsConnected { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public long DatabaseSizeKb { get; set; }
        public int TotalRoutes { get; set; }
        public int TotalStops { get; set; }
        public int TotalOdometerLogs { get; set; }
        public int TotalDrivers { get; set; }
    }

    public interface IDatabaseHealthService
    {
        Task<DatabaseHealthDto> GetHealthStatusAsync();
    }
}
