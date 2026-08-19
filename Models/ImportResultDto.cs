using System.Collections.Generic;

namespace OrionMVP.Models
{
    public class ImportedAddressRecord
    {
        public string ArticleName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class ImportResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public int TotalRead { get; set; }
        public List<ImportedAddressRecord> Records { get; set; } = new();
        public List<string> MissingColumns { get; set; } = new();
    }
}
