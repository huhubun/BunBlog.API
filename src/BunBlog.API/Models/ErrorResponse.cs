namespace BunBlog.API.Models
{
    /// <summary>
    /// 发生错误时的响应
    /// </summary>
    public class ErrorResponse
    {
        public ErrorResponse(string code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
    }
}
