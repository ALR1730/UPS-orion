namespace OrionMVP.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string VehicleId { get; set; } = string.Empty;
        public string Status { get; set; } = "No iniciado"; // "No iniciado", "En Ruta", "Finalizado"
        public List<Route> Routes { get; set; } = new();
    }
}
