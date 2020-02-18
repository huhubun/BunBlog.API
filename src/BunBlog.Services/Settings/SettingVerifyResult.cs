namespace BunBlog.Services.Settings
{
    public class SettingVerifyResult
    {
        public SettingVerifyResult(bool isVerify)
        {
            this.IsVerify = isVerify;
        }

        public SettingVerifyResult(string message)
        {
            this.IsVerify = false;
            this.Message = message;
        }

        public bool IsVerify { get; set; }

        public string Message { get; set; }
    }
}
