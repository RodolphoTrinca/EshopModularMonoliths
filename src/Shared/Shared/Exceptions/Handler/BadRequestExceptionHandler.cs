using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Exceptions.Handler;

public class BadRequestExceptionHandler : ICustomExceptionHandler
{
    public ProblemDetails? Handle(Exception exception)
    {
        if (exception is not BadRequestException)
        {
            return null;
        }

        return new ProblemDetails
        {
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Status = StatusCodes.Status400BadRequest
        };
    }
}
