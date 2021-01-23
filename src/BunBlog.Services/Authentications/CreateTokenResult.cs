namespace BunBlog.Services.Authentications
{
    public class CreateTokenResult
    {
        public CreateTokenResult() { }

        public CreateTokenResult(string errorCode)
        {
            ErrorCode = errorCode;
        }

        public CreateTokenResult(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string RefreshToken { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
