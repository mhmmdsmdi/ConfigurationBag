using ConfigurationBag.EndPoint.Api.Middlewares;

namespace ConfigurationBag.EndPoint.Api.Extensions;

public static class ApiExtensions
{
    public static void UseExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}