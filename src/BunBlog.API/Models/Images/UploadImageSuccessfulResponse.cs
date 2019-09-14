namespace BunBlog.API.Models.Images
{
    /// <summary>
    /// 图片上传成功的响应
    /// </summary>
    public class UploadImageSuccessfulResponse
    {
        /// <summary>
        /// 图片 Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 图片文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 通过该路径可以访问到上传的图片
        /// </summary>
        public string Url { get; set; }
    }
}
