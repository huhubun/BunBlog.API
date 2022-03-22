namespace BunBlog.API.Extensions
{
    public static partial class BunBlogServiceCollectionExtension
    {
        public static IServiceCollection AddBunBlogServices(this IServiceCollection services)
        {
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostMetadataService, PostMetadataService>();
            services.AddScoped<IBunAuthenticationService, BunAuthenticationService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<ISiteLinkService, SiteLinkService>();

            return services;
        }
    }
}
