using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace LMSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        [HttpGet("getfile")]
        public async Task<IActionResult> GetFile(string path)
        {
            try
            {
            var fileName = System.IO.Path.GetFileName(path);
            var content = await System.IO.File.ReadAllBytesAsync(path);
            new FileExtensionContentTypeProvider()
                .TryGetContentType(fileName, out string contentType);
            return File(content, contentType, fileName); 
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
