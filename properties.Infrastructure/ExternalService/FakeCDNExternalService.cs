using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using properties.Domain.Interfaces;
using System.Net.Http;

namespace properties.Infrastructure.ExternalService
{
    public class FakeCDNExternalService : IFakeCDNExternalService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public FakeCDNExternalService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }


        /// <summary>
        /// Simulate upload file a cdn service online
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<string> saveImageAsync(IFormFile image)
        {
            // Obtener la URL del servidor CDN de la configuración
            var serverCdnUrl = _configuration.GetSection("FakeCDN").Value;

            // Crear contenido de la solicitud multipart/form-data
            using var content = new MultipartFormDataContent();
            using var imageStream = image.OpenReadStream();
            content.Add(new StreamContent(imageStream), "image", image.FileName);

            // Enviar solicitud POST al servidor CDN
            var response = await _httpClient.PostAsync(serverCdnUrl+ "/UploadImage", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error Upload iamge to CDN.");
            }

            var path = await response.Content.ReadAsStringAsync();
            return path;
        }
    }
}
