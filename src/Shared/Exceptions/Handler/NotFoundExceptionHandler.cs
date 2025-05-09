using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Exceptions.Handler;

public class NotFoundExceptionHandler : ICustomExceptionHandler
{
    public ProblemDetails? Handle(Exception exception)
    {
        if (exception is not NotFoundException)
        {
            return null;
        }

        return new ProblemDetails
        {
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Status = StatusCodes.Status404NotFound
        };
    }
}
