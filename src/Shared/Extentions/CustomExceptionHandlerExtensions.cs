using Microsoft.Extensions.DependencyInjection;
using Shared.Exceptions.Handler;

namespace Shared.Extensions;

public static class CustomExceptionHandlerExtensions
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddTransient<ICustomExceptionHandler, NotFoundExceptionHandler>();
        services.AddTransient<ICustomExceptionHandler, BadRequestExceptionHandler>();
        services.AddTransient<ICustomExceptionHandler, ValidationExceptionHandler>();
        services.AddTransient<ICustomExceptionHandler, DefaultExceptionHandler>();

        return services;
    }
}
