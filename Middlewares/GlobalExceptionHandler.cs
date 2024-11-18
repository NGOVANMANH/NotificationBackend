using System.Net;
using System.Text.Json;
using notify.Exceptions;

namespace notify.Middlewares;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    private readonly string _environment;

    public GlobalExceptionHandler(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _environment = configuration.GetSection("Environment").Value ?? "Development";
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, _environment);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, string environment)
    {
        HttpStatusCode code = HttpStatusCode.InternalServerError;
        if (exception is GlobalException globalException)
        {
            code = globalException.StatusCode;
        }
        var result = JsonSerializer.Serialize(new
        {
            error = exception.Message,
            stackTrace = environment == "Development" ? exception.StackTrace : null
        });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}