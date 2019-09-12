using Microsoft.AspNetCore.Http;

namespace BunBlog.API.Models.Images
{
    /// <summary>
    /// 用于上传图片的请求
    /// </summary>
    public class UploadImageRequest
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public IFormFile Image { get; set; }
    }
}
