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

            // Seed Data: 5 Conductores Fijos del Piloto (HU01 / Scrum Document)
            modelBuilder.Entity<Driver>().HasData(
                new Driver { Id = 1, Name = "Carlos Santana", VehicleId = "UPS-TRUCK-101", Status = "No iniciado" },
                new Driver { Id = 2, Name = "Manuel Rodríguez", VehicleId = "UPS-VAN-204", Status = "No iniciado" },
                new Driver { Id = 3, Name = "José Gómez", VehicleId = "UPS-TRUCK-309", Status = "No iniciado" },
                new Driver { Id = 4, Name = "David Martínez", VehicleId = "UPS-VAN-412", Status = "No iniciado" },
                new Driver { Id = 5, Name = "Pedro Almonte", VehicleId = "UPS-TRUCK-515", Status = "No iniciado" }
            );

            modelBuilder.Entity<HistoricalRoute>().HasData(
                new HistoricalRoute { Id = 1, RouteName = "Ruta Piloto Zona Urbana Centro", AverageDistanceKm = 52.0, RecordedStopsCount = 15 },
                new HistoricalRoute { Id = 2, RouteName = "Ruta Piloto Zona Metropolitana", AverageDistanceKm = 68.0, RecordedStopsCount = 20 }
            );
        }
    }
}
