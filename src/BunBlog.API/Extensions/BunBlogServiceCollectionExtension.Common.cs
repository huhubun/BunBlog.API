using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace BunBlog.API.Extensions
{
    public static partial class BunBlogServiceCollectionExtension
    {
        public static IServiceCollection AddBunBlog(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddBunBlogApi();
            services.AddBunBlogServices();
            services.AddBunBlogAuthentication(configuration);
            services.AddBunBlogDatabase(configuration);
            services.AddBunBlogSettings(configuration);

            services.AddMemoryCache();
            services.AddAutoMapper(Assembly.GetEntryAssembly());

            return services;
        }

        public static IServiceCollection AddBunBlogApi(this IServiceCollection services)
        {
            services
                .AddControllers()
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
                    fv.RegisterValidatorsFromAssembly(Assembly.GetEntryAssembly());
                });


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

            return services;

        }

        public static IServiceCollection AddBunBlogDatabase(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<BunBlogContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("BunBlogConnection"), npgsqlOptions =>
            {
                npgsqlOptions.MigrationsHistoryTable("MigrationHistory");
            }));
            return services;
        }

        public static IServiceCollection AddBunBlogAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            var authenticationConfig = configuration.GetSection("Authentication").Get<AuthenticationConfig>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = JwtTokenHelper.CreateTokenValidationParameters(
                    authenticationConfig.Issuer,
                    authenticationConfig.Audience,
                    authenticationConfig.Secret
                );

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var authenticationService = context.HttpContext.RequestServices.GetService<IBunAuthenticationService>();

                        var securityToken = (JwtSecurityToken)context.SecurityToken;
                        var username = JwtTokenHelper.GetUserName(securityToken);

                        if (authenticationService.CheckAlreadyEndSessionAccessToken(username, securityToken.RawSignature))
                        {
                            context.Fail("This token already end session.");
                        }
                        else
                        {
                            context.Success();
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        public static IServiceCollection AddBunBlogSettings(this IServiceCollection services, ConfigurationManager configuration)
        {
            var authenticationConfig = configuration.GetSection("Authentication").Get<AuthenticationConfig>();

            #region Load appsettings.json entity to singleton

            services.AddSingleton<AuthenticationConfig>(service =>
            {
                BunAuthenticationService.AuthenticationConfigValidate(authenticationConfig);
                return authenticationConfig;
            });

            services.AddSingleton<UploadImageConfig>(service =>
            {
                return configuration.GetSection("UploadImage").Get<UploadImageConfig>();
            });

            #endregion
            #region Add Cors

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

                options.AddPolicy(CorsPolicy.NAME, builder => builder.WithOrigins(origins.ToArray()).AllowAnyMethod().AllowAnyHeader());
            });

            #endregion

            return services;
        }
    }
}
