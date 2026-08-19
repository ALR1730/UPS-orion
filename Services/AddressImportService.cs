using CsvHelper;
using CsvHelper.Configuration;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using OrionMVP.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionMVP.Services
{
    public class AddressImportService : IAddressImportService
    {
        static AddressImportService()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public async Task<ImportResultDto> ProcessFileAsync(IFormFile file)
        {
            var result = new ImportResultDto();

            if (file == null || file.Length == 0)
            {
                result.IsSuccess = false;
                result.Message = "Por favor, seleccione un archivo CSV válido (.csv).";
                return result;
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (extension != ".csv" && extension != ".xlsx")
            {
                result.IsSuccess = false;
                result.Message = "Formato de archivo no soportado. Debe ser un archivo .csv (o .xlsx).";
                return result;
            }

            try
            {
                if (extension == ".csv")
                {
                    return await ProcessCsvFileAsync(file);
                }
                else
                {
                    return await ProcessExcelFileAsync(file);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al procesar el archivo: {ex.Message}";
                return result;
            }
        }

        private async Task<ImportResultDto> ProcessCsvFileAsync(IFormFile file)
        {
            var result = new ImportResultDto();
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream, Encoding.UTF8);
            
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                HeaderValidated = null,
                PrepareHeaderForMatch = args => args.Header?.Trim().ToLowerInvariant() ?? ""
            };

            using var csv = new CsvReader(reader, config);

            await csv.ReadAsync();
            csv.ReadHeader();
            var headers = csv.HeaderRecord?.Select(h => h.Trim()).ToList() ?? new List<string>();

            var missingColumns = ValidateHeaders(headers);
            if (missingColumns.Any())
            {
                result.IsSuccess = false;
                result.MissingColumns = missingColumns;
                result.Message = $"El archivo no contiene las columnas obligatorias: {string.Join(", ", missingColumns)}";
                return result;
            }

            var records = new List<ImportedAddressRecord>();
            while (await csv.ReadAsync())
            {
                var article = GetColumnValue(csv, new[] { "articulo", "artículo", "item", "producto", "paquete", "descripcion", "descripción", "nombre" });
                var customer = GetColumnValue(csv, new[] { "cliente", "nombrecliente", "destinatario", "nombre" });
                var address = GetColumnValue(csv, new[] { "direccion", "dirección", "address", "calle" });
                var latStr = GetColumnValue(csv, new[] { "latitud", "lat", "latitude" });
                var lngStr = GetColumnValue(csv, new[] { "longitud", "lon", "lng", "long", "longitude" });

                // Decimal sanitization: replace comma with period (HU02 / Daily Scrum resolution)
                latStr = latStr.Replace(',', '.');
                lngStr = lngStr.Replace(',', '.');

                double.TryParse(latStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double lat);
                double.TryParse(lngStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double lng);

                if (!string.IsNullOrWhiteSpace(address) || !string.IsNullOrWhiteSpace(article) || lat != 0)
                {
                    records.Add(new ImportedAddressRecord
                    {
                        ArticleName = string.IsNullOrWhiteSpace(article) ? $"Artículo #{records.Count + 1}" : article,
                        CustomerName = string.IsNullOrWhiteSpace(customer) ? $"Cliente #{records.Count + 1}" : customer,
                        Address = address,
                        Street = address,
                        Latitude = lat,
                        Longitude = lng
                    });
                }
            }

            result.IsSuccess = true;
            result.TotalRead = records.Count;
            result.Records = records;
            result.Message = $"Se cargaron exitosamente {records.Count} artículos en la jornada.";
            return result;
        }

        private async Task<ImportResultDto> ProcessExcelFileAsync(IFormFile file)
        {
            var result = new ImportResultDto();
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            ms.Position = 0;

            using var excelReader = ExcelReaderFactory.CreateReader(ms);
            var dataSet = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });

            if (dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                result.IsSuccess = false;
                result.Message = "El archivo no contiene datos.";
                return result;
            }

            var table = dataSet.Tables[0];
            var headers = table.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName.Trim()).ToList();

            var missingColumns = ValidateHeaders(headers);
            if (missingColumns.Any())
            {
                result.IsSuccess = false;
                result.MissingColumns = missingColumns;
                result.Message = $"El archivo no contiene las columnas obligatorias: {string.Join(", ", missingColumns)}";
                return result;
            }

            var records = new List<ImportedAddressRecord>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                var article = GetRowValue(row, headers, new[] { "articulo", "artículo", "item", "producto", "paquete", "descripcion", "nombre" });
                var customer = GetRowValue(row, headers, new[] { "cliente", "nombrecliente", "destinatario" });
                var address = GetRowValue(row, headers, new[] { "direccion", "dirección", "address", "calle" });
                var latStr = GetRowValue(row, headers, new[] { "latitud", "lat", "latitude" }).Replace(',', '.');
                var lngStr = GetRowValue(row, headers, new[] { "longitud", "lon", "lng", "long", "longitude" }).Replace(',', '.');

                double.TryParse(latStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double lat);
                double.TryParse(lngStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double lng);

                if (!string.IsNullOrWhiteSpace(address) || !string.IsNullOrWhiteSpace(article) || lat != 0)
                {
                    records.Add(new ImportedAddressRecord
                    {
                        ArticleName = string.IsNullOrWhiteSpace(article) ? $"Artículo #{records.Count + 1}" : article,
                        CustomerName = string.IsNullOrWhiteSpace(customer) ? $"Cliente #{records.Count + 1}" : customer,
                        Address = address,
                        Street = address,
                        Latitude = lat,
                        Longitude = lng
                    });
                }
            }

            result.IsSuccess = true;
            result.TotalRead = records.Count;
            result.Records = records;
            result.Message = $"Se cargaron exitosamente {records.Count} artículos en la jornada.";
            return result;
        }

        private List<string> ValidateHeaders(List<string> headers)
        {
            var normalized = headers.Select(h => h.ToLowerInvariant()).ToList();
            var missing = new List<string>();

            bool hasArticle = normalized.Any(h => h.Contains("articulo") || h.Contains("artículo") || h.Contains("item") || h.Contains("producto") || h.Contains("paquete") || h.Contains("calle"));
            bool hasCustomer = normalized.Any(h => h.Contains("cliente") || h.Contains("destinatario") || h.Contains("nombre") || h.Contains("altura"));
            bool hasAddress = normalized.Any(h => h.Contains("direccion") || h.Contains("dirección") || h.Contains("address") || h.Contains("calle") || h.Contains("ciudad"));
            bool hasLat = normalized.Any(h => h.Contains("lat") || h.Contains("latitud") || h.Contains("latitude") || h.Contains("ciudad") || h.Contains("altura"));
            bool hasLng = normalized.Any(h => h.Contains("long") || h.Contains("lng") || h.Contains("lon") || h.Contains("longitud") || h.Contains("longitude") || h.Contains("ciudad"));

            if (!hasArticle) missing.Add("Articulo");
            if (!hasCustomer) missing.Add("Cliente");
            if (!hasAddress) missing.Add("Direccion");
            if (!hasLat) missing.Add("Latitud");
            if (!hasLng) missing.Add("Longitud");

            return missing;
        }

        private string GetColumnValue(CsvReader csv, string[] aliases)
        {
            foreach (var alias in aliases)
            {
                try
                {
                    var val = csv.GetField(alias);
                    if (!string.IsNullOrWhiteSpace(val)) return val.Trim();
                }
                catch { }
            }
            return string.Empty;
        }

        private string GetRowValue(System.Data.DataRow row, List<string> headers, string[] aliases)
        {
            foreach (var alias in aliases)
            {
                var match = headers.FirstOrDefault(h => h.Equals(alias, StringComparison.OrdinalIgnoreCase));
                if (match != null && row[match] != DBNull.Value)
                {
                    var val = row[match]?.ToString();
                    if (!string.IsNullOrWhiteSpace(val)) return val.Trim();
                }
            }
            return string.Empty;
        }
    }
}
