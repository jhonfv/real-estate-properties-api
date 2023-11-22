using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace properties.FakeCDN.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadImage")]
        public async Task<string> UploadImage(IFormFile image)
        {
            var paht = Path.Combine("static","files",$"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", paht);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return paht.Replace("\\", "/");
        }
    }
}