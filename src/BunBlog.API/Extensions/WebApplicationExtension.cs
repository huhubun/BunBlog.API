using Microsoft.AspNetCore.Diagnostics;

namespace BunBlog.API.Extensions
{
    public static class WebApplicationExtension
    {
        public static WebApplication UseBunBlog(this WebApplication app)
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

            app.UseCors(CorsPolicy.NAME);

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

            return app;
        }
    }
}
