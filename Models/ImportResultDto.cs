using System.Collections.Generic;

namespace OrionMVP.Models
{
    public class ImportedAddressRecord
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty; // Altura
        public string City { get; set; } = string.Empty;
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
