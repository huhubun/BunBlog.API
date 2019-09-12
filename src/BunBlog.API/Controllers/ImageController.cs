using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BunBlog.API.Models.Images;
using BunBlog.Services.Images;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunBlog.API.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>
        /// 上传图片（本协议需要使用 multipart/form-data 作为 Content-Type）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<IActionResult> UploadImage([FromForm]UploadImageRequest request)
        {
            using (var imageStream = request.Image.OpenReadStream())
            {
                // GetExtension() 的结果包含点号，如 ".jpg"
                var extension = Path.GetExtension(request.FileName);

                var url = await _imageService.Upload(extension, request.Description, imageStream);

                return Ok(new UploadImageSuccessfulResponse
                {
                    Url = url
                });
            }
        }
    }
}