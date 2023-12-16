using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Behaviors;

public class ValidationExceptionMiddleware(RequestDelegate next, ILogger<ValidationExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "ValidationFailure",
                Title = "Validation error",
                Detail = "One or more validation errors has occurred"
            };

            if (exception.Errors is not null) problemDetails.Extensions["errors"] = exception.Errors;

            logger.LogError("{Error}", JsonConvert.SerializeObject(problemDetails));

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
                await context.Response.WriteAsync("An error has occured");
            else
                await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}