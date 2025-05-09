using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Exceptions.Handler;

public class ValidationExceptionHandler : ICustomExceptionHandler
{
    public ProblemDetails? Handle(Exception exception)
    {
        if (exception is not ValidationException validationException)
        {
            return null;
        }

        var problemDetails = new ProblemDetails
        {
            Title = "Validation Error",
            Detail = exception.Message,
            Status = StatusCodes.Status400BadRequest
        };

        problemDetails.Extensions.Add("ValidationErros", validationException.Errors);

        return problemDetails;
    }
}
