using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCore.Api.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IHostingEnvironment _environment;

        public ImageController(IHostingEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        // POST: api/Image
        [HttpPost("Image")]
        public async Task Post(IFormFile file)
        {
            if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
            {
                _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            var uploads = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            string folder = "Images";
            List<string> filePaths = new List<string>();

            var uploads = Path.Combine(_environment.WebRootPath, folder);

            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);


            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var path = folder+"/" + formFile.FileName;

                    filePaths.Add(path);

                    using (var stream = new FileStream(Path.Combine(uploads, formFile.FileName), FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, filePaths });
        }
    }

}