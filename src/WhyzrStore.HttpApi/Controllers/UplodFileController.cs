using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using ImageMagick;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace WhyzrStore.Controllers
{
    [Route("api/upload")]
    public class UploadController : WhyzrStoreController
    {
        [HttpPost]
        [Route("image2")]
        public async Task<IActionResult> UploadByFile2(IFormFile file2)
        {
            string url = "";

            // generate new unique file name
            FileInfo fi = new FileInfo(file2.FileName);

            string fileFullName = Guid.NewGuid() + fi.Extension;

            var filePath1 = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", "ImageMagick" + fileFullName);
            var stream = new MemoryStream();
            using (var stream2 = new MemoryStream())
            {
                file2.CopyTo(stream);
                stream.Seek(0, SeekOrigin.Begin);
                var optimizer = new ImageOptimizer();

                using (var image2 = new MagickImage(stream))
                {
                    image2.Resize(700,700);
                    image2.Write(filePath1);
                }

                optimizer.LosslessCompress(filePath1);
            }


            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileFullName);
           

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {

                await file2.CopyToAsync(fileStream);
            }

            url = $"images/{fileFullName}";

            return Ok(new { url });
        }


        [HttpPost]
        [Route("image")]
        public async Task<IActionResult> UploadByFile(IFormFile file)
        {
            string url = "";

            // generate new unique file name
            FileInfo fi = new FileInfo(file.FileName);

            string fileFullName = Guid.NewGuid() + fi.Extension;
            var filePath1 = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", "ImageSharp" + fileFullName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileFullName);
            using var image = Image.Load(file.OpenReadStream());
            image.Mutate(x => x.Resize(700, 700));
            var encoder = new JpegEncoder()
            {
                Quality = 30 //Use variable to set between 5-30 based on your requirements
            };

            image.Save(filePath1,encoder);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {

                await file.CopyToAsync(fileStream);
            }

            url = $"images/{fileFullName}";

            return Ok(new { url });
        }
    }
}