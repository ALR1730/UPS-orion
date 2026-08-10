using Microsoft.EntityFrameworkCore;
using OrionMVP.Models;
using System;
using Route = OrionMVP.Models.Route;

namespace OrionMVP.Data
{
    public class OrionDbContext : DbContext
    {
        public OrionDbContext(DbContextOptions<OrionDbContext> options) : base(options) { }

        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<Route> Routes => Set<Route>();
        public DbSet<RouteStop> RouteStops => Set<RouteStop>();
        public DbSet<OdometerLog> OdometerLogs => Set<OdometerLog>();
        public DbSet<HistoricalRoute> HistoricalRoutes => Set<HistoricalRoute>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Route>()
                .HasOne(r => r.Driver)
                .WithMany(d => d.Routes)
                .HasForeignKey(r => r.DriverId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RouteStop>()
                .HasOne(rs => rs.Route)
                .WithMany(r => r.Stops)
                .HasForeignKey(rs => rs.RouteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data
            modelBuilder.Entity<Driver>().HasData(
                new Driver { Id = 1, Name = "Carlos Mendoza", VehicleId = "UPS-TRUCK-101", Status = "En Ruta" },
                new Driver { Id = 2, Name = "Ana Gomez", VehicleId = "UPS-VAN-204", Status = "No iniciado" },
                new Driver { Id = 3, Name = "Roberto Fernandez", VehicleId = "UPS-TRUCK-309", Status = "Finalizado" }
            );

            modelBuilder.Entity<HistoricalRoute>().HasData(
                new HistoricalRoute { Id = 1, RouteName = "Ruta Centro Urbano", AverageDistanceKm = 145.0, RecordedStopsCount = 35 },
                new HistoricalRoute { Id = 2, RouteName = "Ruta Suburbana Norte", AverageDistanceKm = 210.0, RecordedStopsCount = 42 }
            );
        }
    }
}
