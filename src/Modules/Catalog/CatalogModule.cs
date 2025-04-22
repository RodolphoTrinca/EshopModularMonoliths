using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddApplicationServices()
        //    .AddInfrastructureServices(configuration)
        //    .AddApiServices(configuration);

        return services;
    }
}
