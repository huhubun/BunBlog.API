using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Bun.Blog.Web.Extensions
{
    public static class IRouteBuilderExtensions
    {
        public static IRouteBuilder MapAdminRoute(this IRouteBuilder routeBuilder)
        {
            return routeBuilder
                    .MapAdminAreaRoute(
                        name: "Admin",
                        url: "",
                        defaults: new { controller = "Dashboard", action = "Index" }
                    )
                    .MapAdminAreaRoute(
                        name: "Dashboard",
                        url: "Dashboard",
                        defaults: new { controller = "Dashboard", action = "Index" }
                    )
                    .MapAdminAreaRoute(
                        name: "PostList",
                        url: "Post/List",
                        defaults: new { controller = "Post", action = "List" }
                    )
                    .MapAdminAreaRoute(
                        name: "PostNew",
                        url: "PostNew",
                        defaults: new { controller = "Post", action = "New" }
                    )
                    .MapAdminAreaRoute(
                        name: "EditPost",
                        url: "Post/Edit/{id:int}",
                        defaults: new { controller = "Post", action = "Edit" }
                    );
        }

        private static IRouteBuilder MapAdminAreaRoute(this IRouteBuilder routeBuilder, string name, string url, object defaults)
        {
            return routeBuilder.MapAreaRoute(name, "Admin", "Admin/" + url, defaults);
        }
    }
}
