using BunBlog.API.Models.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        {
            var image = request.Image;
            using (var imageStream = image.OpenReadStream())
            {
                var result = await _imageService.UploadAsync(image.FileName, imageStream);

                return Ok(new UploadImageSuccessfulResponse
                {
                    Id = result.Id,
                    FileName = result.FileName,
                    Url = result.Url
                });
            }
        }
    }
}