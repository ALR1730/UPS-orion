using System.Threading.Tasks;

namespace OrionMVP.Services
{
    public class GeocodeResultDto
    {
        public bool IsSuccess { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public interface IGeocodingService
    {
        Task<GeocodeResultDto> GeocodeAddressAsync(string street, string number, string city);
    }
}
