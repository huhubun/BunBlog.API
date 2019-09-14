using BunBlog.API.Models.Images;
using BunBlog.Core.Configuration;
using BunBlog.Services.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BunBlog.API.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly UploadImageConfig _uploadImageConfig;
        private readonly IImageService _imageService;

        public ImageController(
            UploadImageConfig uploadImageConfig,
            IImageService imageService)
        {
            _uploadImageConfig = uploadImageConfig;
            _imageService = imageService;
        }

        /// <summary>
        /// 上传图片（本协议需要使用 multipart/form-data 作为 Content-Type）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> UploadImage([FromForm]UploadImageRequest request)
        {
            var image = request.Image;
            using (var imageStream = image.OpenReadStream())
            {
                // GetExtension() 的结果包含点号，如 ".jpg"
                var extension = Path.GetExtension(image.FileName);

                var imageEntity = await _imageService.Upload(extension, imageStream);

                var baseImageUrl = new Uri(_uploadImageConfig.Domain);

                return Ok(new UploadImageSuccessfulResponse
                {
                    Id = imageEntity.Id,
                    FileName = imageEntity.FileName,
                    Url = new Uri(baseImageUrl, $"{imageEntity.Path}/{imageEntity.FileName}").ToString()
                });
            }
        }
    }
}