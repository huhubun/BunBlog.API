using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Bun.Blog.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        // https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
