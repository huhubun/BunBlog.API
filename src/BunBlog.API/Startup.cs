using AutoMapper;
using BunBlog.API.Const;
using BunBlog.API.Middleware;
using BunBlog.API.Models;
using BunBlog.Core.Configuration;
using BunBlog.Data;
using BunBlog.Services.Authentications;
using BunBlog.Services.Categories;
using BunBlog.Services.Images;
using BunBlog.Services.Posts;
using BunBlog.Services.Securities;
using BunBlog.Services.Settings;
using BunBlog.Services.SiteLinks;
using BunBlog.Services.Tags;
using FluentValidation.AspNetCore;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BunBlog.API
{
    public class Startup
    {
        private const string CORS_POLICY_NAME = "BUN_BLOG_API_CORS";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services
                .AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new BunBlogValidationProblemDetails(context.ModelState)
                        {
                            TraceId = context.HttpContext.TraceIdentifier
                        };

                        return new BadRequestObjectResult(problemDetails);
                    };
                })
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
                });

            services.AddDbContext<BunBlogContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("BunBlogConnection"), npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable("MigrationHistory");
                }));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Bun Blog API",
                    Version = "v1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            var authenticationConfig = Configuration.GetSection("Authentication").Get<AuthenticationConfig>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,

                    ValidIssuer = authenticationConfig.Issuer,
                    ValidAudience = authenticationConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationConfig.Secret)),

                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
            });

            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostMetadataService, PostMetadataService>();
            services.AddScoped<IBunAuthenticationService, BunAuthenticationService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<ISiteLinkService, SiteLinkService>();

            // appsettings.json 中的配置
            services.AddSingleton<AuthenticationConfig>(service =>
            {
                BunAuthenticationService.AuthenticationConfigValidate(authenticationConfig);
                return authenticationConfig;
            });

            services.AddSingleton<UploadImageConfig>(service =>
            {
                return Configuration.GetSection("UploadImage").Get<UploadImageConfig>();
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddCors(options =>
            {
                var origins = new List<string>
                {
                    "https://bun.plus",
                    "https://bun.dev"
                };

#if DEBUG
                origins.Add("http://localhost:17088");
#endif

                options.AddPolicy(CORS_POLICY_NAME, builder => builder.WithOrigins(origins.ToArray()).AllowAnyMethod().AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRequestResponseLogging();

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;

                    var factory = context.RequestServices.GetService<ILoggerFactory>();
                    var logger = factory.CreateLogger("ExceptionHandler");
                    logger.LogError(exception, String.Empty);

                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var message = JsonConvert.SerializeObject(new ErrorResponse(ErrorResponseCode.SERVER_ERROR, $"服务器端发生错误，请将 {context.TraceIdentifier} 反馈给管理员以协助修复该问题"));
                    await context.Response.WriteAsync(message);
                });
            });

            app.UseRouting();

            app.UseCors(CORS_POLICY_NAME);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // https://docs.microsoft.com/zh-cn/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "docs";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bun Blog API v1");
            });
        }
    }
}
