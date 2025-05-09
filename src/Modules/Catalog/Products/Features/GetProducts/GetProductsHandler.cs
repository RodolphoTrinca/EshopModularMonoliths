using Catalog.Products.Features.GetProductByCategory;

namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery()
    : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<ProductDTO> Products);

internal class GetProductsHandler(CatalogDbContext dbContext, ILogger<GetProductsHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        logger.LogDebug($"Fetched {products.Count} products from the database, converting them to DTOs");

        var productDtos = products.Adapt<List<ProductDTO>>();

        return new GetProductsResult(productDtos);
    }
}
