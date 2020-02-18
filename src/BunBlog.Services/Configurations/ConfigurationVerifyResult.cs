namespace BunBlog.Services.Configurations
{
    public class ConfigurationVerifyResult
    {
        public ConfigurationVerifyResult(bool isVerify)
        {
            this.IsVerify = isVerify;
        }

        public ConfigurationVerifyResult(string message)
        {
            this.IsVerify = false;
            this.Message = message;
        }

        public bool IsVerify { get; set; }

        public string Message { get; set; }
    }
}
