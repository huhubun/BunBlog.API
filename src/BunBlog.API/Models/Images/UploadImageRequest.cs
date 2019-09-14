using Microsoft.AspNetCore.Http;

namespace BunBlog.API.Models.Images
{
    /// <summary>
    /// 用于上传图片的请求
    /// </summary>
    public class UploadImageRequest
    {
        /// <summary>
        /// 图片
        /// </summary>
        public IFormFile Image { get; set; }
    }
}
