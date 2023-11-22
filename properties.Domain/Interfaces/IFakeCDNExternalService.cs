using Microsoft.AspNetCore.Http;

namespace properties.Domain.Interfaces
{
    public interface IFakeCDNExternalService
    {
        public Task<string> saveImageAsync(IFormFile image);
    }
}
