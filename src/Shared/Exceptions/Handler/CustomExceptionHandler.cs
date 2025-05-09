using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shared.Exceptions.Handler;

public interface ICustomExceptionHandler
{
    ProblemDetails? Handle(Exception exception);
}

public class CustomExceptionHandler(
    ILogger<CustomExceptionHandler> logger,
    IEnumerable<ICustomExceptionHandler> handlers) 
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError($"Error Message: {exception.Message}, Time of occurrence {DateTime.UtcNow}");

        using var handler = handlers.GetEnumerator();
        ProblemDetails problemDetails = null;
        while (handler.MoveNext())
        {
            problemDetails = handler.Current.Handle(exception);
            if (problemDetails is not null)
            {
                break;
            }
        }

        problemDetails!.Instance = context.Request.Path;
        context.Response.StatusCode = problemDetails!.Status!.Value;
        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
}
