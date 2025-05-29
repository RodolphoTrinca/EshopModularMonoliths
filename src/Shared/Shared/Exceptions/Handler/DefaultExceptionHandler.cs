using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Exceptions.Handler;

public class DefaultExceptionHandler : ICustomExceptionHandler
{
    public ProblemDetails? Handle(Exception exception)
    {
        return new ProblemDetails
        {
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Status = StatusCodes.Status500InternalServerError
        };
    }
}
