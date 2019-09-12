namespace BunBlog.API.Models.Images
{
    /// <summary>
    /// 图片上传成功的响应
    /// </summary>
    public class UploadImageSuccessfulResponse
    {
        /// <summary>
        /// 通过该路径可以访问到上传的图片
        /// </summary>
        public string Url { get; set; }
    }
}
