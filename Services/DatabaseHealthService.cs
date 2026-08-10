using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrionMVP.Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OrionMVP.Services
{
    public class DatabaseHealthService : IDatabaseHealthService
    {
        private readonly OrionDbContext _db;
        private readonly IConfiguration _config;

        public DatabaseHealthService(OrionDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<DatabaseHealthDto> GetHealthStatusAsync()
        {
            var dto = new DatabaseHealthDto
            {
                Provider = "SQLite / EF Core 10",
                ConnectionString = _config.GetConnectionString("DefaultConnection") ?? "Data Source=orion_pilot.db"
            };

            try
            {
                dto.IsConnected = await _db.Database.CanConnectAsync();
                
                dto.TotalRoutes = await _db.Routes.CountAsync();
                dto.TotalStops = await _db.RouteStops.CountAsync();
                dto.TotalOdometerLogs = await _db.OdometerLogs.CountAsync();
                dto.TotalDrivers = await _db.Drivers.CountAsync();

                var dbPath = "orion_pilot.db";
                if (!File.Exists(dbPath))
                {
                    dbPath = "orion.db";
                }

                if (File.Exists(dbPath))
                {
                    var fileInfo = new FileInfo(dbPath);
                    dto.DatabaseSizeKb = fileInfo.Length / 1024;
                }
            }
            catch (Exception)
            {
                dto.IsConnected = false;
            }

            return dto;
        }
    }
}
