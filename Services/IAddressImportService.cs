using Microsoft.AspNetCore.Http;
using OrionMVP.Models;
using System.Threading.Tasks;

namespace OrionMVP.Services
{
    public interface IAddressImportService
    {
        Task<ImportResultDto> ProcessFileAsync(IFormFile file);
    }
}
