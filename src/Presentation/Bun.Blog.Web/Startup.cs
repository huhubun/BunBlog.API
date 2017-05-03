using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Razor;
using Bun.Blog.Web.Extensions;
using Microsoft.Extensions.Configuration;
using Bun.Blog.Data;
using Microsoft.EntityFrameworkCore;
using Bun.Blog.Core.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Bun.Blog.Web.Models.Accounts;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Web.Models.Posts;
using Bun.Blog.Services.Posts;
using Bun.Blog.Core.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Bun.Blog.Web
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
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // MVC and route
            services.AddMvc();
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
                options.UseNpgsql("Host=bun-v-server;Database=SampleDb;Username=postgres;Password=http://nzc.me");
            });

            // Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BlogContext>()
                .AddDefaultTokenProviders();
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

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/Login";
                options.Cookies.ApplicationCookie.LogoutPath = "/LogOff";

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddAutoMapper(config => config.CreateBunBlogMap());
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

            services.AddScoped<IPostService, PostService>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIdentity();

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
                    .MapAdminRoute();
            });
        }
    }
}