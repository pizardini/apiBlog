using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class KeyMiddleware
{
    private const string KeyName = "x-api-key";

    private readonly RequestDelegate _next;

    public KeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
{
    // string DesiredKey = context.RequestServices.GetRequiredService<IConfiguration>().GetValue<string>(KeyName);

    // if (!context.Request.Headers.TryGetValue(KeyName, out Microsoft.Extensions.Primitives.StringValues value))
    // {
    //     context.Response.StatusCode = 401;
    //     await context.Response.WriteAsync("Chave não informada");
    //     return;
    // }

    // if (DesiredKey != value)
    // {
    //     context.Response.StatusCode = 401;
    //     await context.Response.WriteAsync("Acesso não autorizado");
    //     return;
    // }

    await _next(context);
}
}

public static class KeyMiddlewareExtensions
{
    public static IApplicationBuilder UseAPIKey(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<KeyMiddleware>();
    }
}