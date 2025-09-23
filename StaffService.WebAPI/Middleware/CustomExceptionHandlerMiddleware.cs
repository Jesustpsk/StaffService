using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StaffService.Application.Common.Exceptions;

namespace StaffService.WebAPI.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case ValidationException validationEx:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Title = $"Validation error";
                problemDetails.Extensions["errors"] = validationEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToArray());
                _logger.LogError(validationEx, "Validation error occurred");
                break;

            case FileNotFoundException:
            case NotFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                problemDetails.Title = "Resource not found";
                _logger.LogError(exception, "Resource not found");
                break;
            
            case ArgumentOutOfRangeException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Argument out of range";
                problemDetails.Extensions["errors"] = exception.Message;
                _logger.LogError(exception, "Argument out of range");
                break;
            
            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Internal server error";
                _logger.LogError(exception, "Unhandled exception occurred");
                break;
        }
        if (_env.IsDevelopment())
        {
            problemDetails.Detail = exception.ToString();
        }

        problemDetails.Status = context.Response.StatusCode;
        problemDetails.Type = $"https://httpstatuses.com/{problemDetails.Status}";
        
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}