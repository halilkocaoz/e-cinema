using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace ECinema.Common;

public class ExceptionsMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ApiException exception)
        {
            context.Response.StatusCode = exception.Code;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                message = $"Api exception: {exception.Message}"
            }));
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                message = $"Unhandled exception: {exception.Message}"
            }));
        }
    }
}