using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Api.Common.Models;

namespace Api.Common.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {

            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        var defaultStatusCode = HttpStatusCode.InternalServerError;

        var errorDetails = new ExceptionDetails()
        {
            ErrorMessage = exception.Message,
            ErrorType = exception.GetType().Name,
            TraceId = context.TraceIdentifier
        };

        var result = JsonSerializer.Serialize(errorDetails);

        context.Response.StatusCode = (int)defaultStatusCode;

        return context.Response.WriteAsync(result);
    }
}