using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Shared.Behaviors;
public class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation($"[Start] Handling request: {typeof(TRequest).Name} - Response{typeof(TResponse).Name}" );
        logger.LogDebug($"Request Data: {request}");

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();

        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning($"[Performance] The request {typeof(TRequest).Name} took {timeTaken.Seconds} seconds.");
        }

        logger.LogInformation($"[End] Handled request: {typeof(TRequest).Name} - Response{typeof(TResponse).Name} - Elapsed Time: {timer.ElapsedMilliseconds} ms");
        logger.LogDebug($"Response Data: {response}");

        return response;
    }
}
