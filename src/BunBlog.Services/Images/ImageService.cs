using BunBlog.Core.Configuration;
using BunBlog.Core.Domain.Images;
using BunBlog.Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BunBlog.Services.Images
{
    public class ImageService : IImageService
    {
        private readonly UploadImageConfig _uploadImageConfig;
        private readonly BunBlogContext _bunBlogContext;

        public ImageService(
            BunBlogContext bunBlogContext,
            UploadImageConfig uploadImageConfig
            )
        {
            _bunBlogContext = bunBlogContext;
            _uploadImageConfig = uploadImageConfig;
        }

        public async Task<Image> Upload(string extension, Stream imageStream)
        {
            var now = DateTime.Now;

            var rootPath = _uploadImageConfig.SavePath;
            // 以日期为依据生成储存图片的路径，结果类似于 2019\\09\\15 
            var saveImageFilePath = GenerateSaveImageFilePath(now);
            var fileName = $"{Guid.NewGuid().ToString()}{extension}";

            var path = Path.Combine(rootPath, saveImageFilePath);

            var saveImageFileTask = SaveImageFileAsync(path, fileName, imageStream);
            var insertImageEntityTask = InsertImageEntity(saveImageFilePath, fileName);

            await saveImageFileTask;
            var imageEntity = await insertImageEntityTask;

            return imageEntity;
        }

        private async Task SaveImageFileAsync(string path, string fileName, Stream imageStream)
        {
            Directory.CreateDirectory(path);

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await imageStream.CopyToAsync(fileStream);
            }
        }

        private async Task<Image> InsertImageEntity(string path, string fileName)
        {
            var image = new Image
            {
                Path = path,
                FileName = fileName,
                UploadTime = DateTime.Now
            };

            await _bunBlogContext.Images.AddAsync(image);
            await _bunBlogContext.SaveChangesAsync();

            return image;
        }

        private string GenerateSaveImageFilePath(DateTime date)
        {
            return Path.Combine(date.Year.ToString(), date.Month.ToString(), date.Day.ToString());
        }

        private string GenerateImageUrlPath(DateTime date)
        {
            return $"{date.Year}/{date.Month}/{date.Day}";
        }
    }
}
