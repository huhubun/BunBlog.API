namespace BunBlog.Services.Settings
{
    public class SettingsVerifyResult
    {
        public SettingsVerifyResult(bool isVerify)
        {
            this.IsVerify = isVerify;
        }

        public SettingsVerifyResult(string message)
        {
            this.IsVerify = false;
            this.Message = message;
        }

        public bool IsVerify { get; set; }

        public string Message { get; set; }
    }
}
