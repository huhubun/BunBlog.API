using NLog.Web;

namespace BunBlog.API.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static void ConfigNLog(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(LogLevel.Trace);
            builder.Host.UseNLog();
        }
    }
}
