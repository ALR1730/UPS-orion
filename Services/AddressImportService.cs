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
                result.Message = "Por favor, seleccione un archivo válido (.csv o .xlsx).";
                return result;
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (extension != ".csv" && extension != ".xlsx")
            {
                result.IsSuccess = false;
                result.Message = "Formato de archivo no soportado. Debe ser .csv o .xlsx.";
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
                var street = GetColumnValue(csv, new[] { "calle", "direccion", "dirección" });
                var number = GetColumnValue(csv, new[] { "altura", "numero", "número" });
                var city = GetColumnValue(csv, new[] { "ciudad", "localidad" });
                var customer = GetColumnValue(csv, new[] { "cliente", "nombrecliente", "nombre" });

                if (!string.IsNullOrWhiteSpace(street) || !string.IsNullOrWhiteSpace(city))
                {
                    records.Add(new ImportedAddressRecord
                    {
                        Street = street,
                        Number = number,
                        City = city,
                        CustomerName = string.IsNullOrWhiteSpace(customer) ? $"Cliente #{records.Count + 1}" : customer
                    });
                }
            }

            result.IsSuccess = true;
            result.TotalRead = records.Count;
            result.Records = records;
            result.Message = $"Se leyeron exitosamente {records.Count} direcciones del archivo CSV.";
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
                result.Message = "El archivo de Excel no contiene datos o la hoja está vacía.";
                return result;
            }

            var table = dataSet.Tables[0];
            var headers = table.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName.Trim()).ToList();

            var missingColumns = ValidateHeaders(headers);
            if (missingColumns.Any())
            {
                result.IsSuccess = false;
                result.MissingColumns = missingColumns;
                result.Message = $"El archivo Excel no contiene las columnas obligatorias: {string.Join(", ", missingColumns)}";
                return result;
            }

            var records = new List<ImportedAddressRecord>();
            foreach (System.Data.DataRow row in table.Rows)
            {
                var street = GetRowValue(row, headers, new[] { "calle", "direccion", "dirección" });
                var number = GetRowValue(row, headers, new[] { "altura", "numero", "número" });
                var city = GetRowValue(row, headers, new[] { "ciudad", "localidad" });
                var customer = GetRowValue(row, headers, new[] { "cliente", "nombrecliente", "nombre" });

                if (!string.IsNullOrWhiteSpace(street) || !string.IsNullOrWhiteSpace(city))
                {
                    records.Add(new ImportedAddressRecord
                    {
                        Street = street,
                        Number = number,
                        City = city,
                        CustomerName = string.IsNullOrWhiteSpace(customer) ? $"Cliente #{records.Count + 1}" : customer
                    });
                }
            }

            result.IsSuccess = true;
            result.TotalRead = records.Count;
            result.Records = records;
            result.Message = $"Se leyeron exitosamente {records.Count} direcciones del archivo Excel.";
            return result;
        }

        private List<string> ValidateHeaders(List<string> headers)
        {
            var normalized = headers.Select(h => h.ToLowerInvariant()).ToList();
            var missing = new List<string>();

            if (!normalized.Any(h => h.Contains("calle") || h.Contains("direccion") || h.Contains("dirección")))
                missing.Add("Calle");

            if (!normalized.Any(h => h.Contains("altura") || h.Contains("numero") || h.Contains("número")))
                missing.Add("Altura");

            if (!normalized.Any(h => h.Contains("ciudad") || h.Contains("localidad")))
                missing.Add("Ciudad");

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
