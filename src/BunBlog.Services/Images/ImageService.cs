using BunBlog.Core.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BunBlog.Services.Images
{
    public class ImageService : IImageService
    {
        private readonly UploadImageConfig _uploadImageConfig;

        public ImageService(UploadImageConfig uploadImageConfig)
        {
            _uploadImageConfig = uploadImageConfig;
        }

        public async Task<string> Upload(string extension, string description, Stream imageStream)
        {
            var now = DateTime.Now;

            var pathA = _uploadImageConfig.SavePath;
            var pathB = $"{now.Year}/{now.Month}/{now.Day}";
            var fileName = $"{Guid.NewGuid().ToString()}{extension}";

            var path = Path.Combine(pathA, pathB);

            Directory.CreateDirectory(path);

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await imageStream.CopyToAsync(fileStream);
            }

            return $"{_uploadImageConfig.Domain}/{pathB}/{fileName}";
        }
    }
}
