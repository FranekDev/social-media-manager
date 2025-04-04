﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SocialMediaManager.Api.Middlewares;

public sealed class ValidationExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "ValidationFailure",
                Title = "Validation error",
                Detail = "One or more validation errors occurred",
            };

            if (exception.Errors is not null)
            {
                problemDetails.Extensions["errors"] = exception.Errors;
            }
            
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}