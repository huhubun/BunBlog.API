using AutoMapper;
using Bun.Blog.Core.Data;
using Bun.Blog.Core.Domain.Users;
using Bun.Blog.Data;
using Bun.Blog.Services.Categories;
using Bun.Blog.Services.Posts;
using Bun.Blog.Web.Admin.Extensions;
using Bun.Blog.Web.Admin.Mappers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Reflection;

namespace Bun.Blog.Web.Admin
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            env.ConfigureNLog("nlog.config");

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // MVC and route
            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.Configure<RazorViewEngineOptions>(options =>
            {
                // MVC Areas https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/areas
                // {2}: Area Name
                // {1}: Controller Name
                // {0}: View Name
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            // Entity Framework
            services.AddDbContext<BlogContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("BunBlogConnection"));
            });

            // Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BlogContext>()
                .AddDefaultTokenProviders();

            // https://docs.microsoft.com/zh-cn/aspnet/core/migration/1x-to-2x/identity-2x
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(150);
                    options.LoginPath = "/Login";
                    options.LogoutPath = "/LogOff";
                });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            // Others
            services.AddAutoMapper(config =>
            {
                config.AddProfile<PostMapperProfile>();
                config.AddProfile<UserMapperProfile>();
            });

            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();

            app.AddNLogWeb();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes
                    .MapRoute(
                        name: "Web",
                        template: "",
                        defaults: new { controller = "Home", action = "Index" }
                    )
                    .MapRoute(
                        name: "Login",
                        template: "Login",
                        defaults: new { controller = "Account", action = "Login", area = "Admin" }
                    )
                    .MapRoute(
                        name: "LogOff",
                        template: "LogOff",
                        defaults: new { controller = "Account", action = "LogOff", area = "Admin" }
                    )
                    .MapRoute(
                        name: "Register",
                        template: "Register",
                        defaults: new { controller = "Account", action = "Register", area = "Admin" }
                    )
                    .MapRoute(
                        name: "AdminDefault",
                        template: "Admin/{controller}/{action}/{id?}",
                        defaults: new { controller = "Dashboard", action = "Index", area = "Admin" }
                    )
                    .MapAdminRoute();
            });
        }
    }
}