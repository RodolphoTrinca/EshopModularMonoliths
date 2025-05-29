using Catalog.Contracts.Products.Features.GetProductById;

namespace Catalog.Products.Features.GetProductById;

internal class GetProductByIdHandler(CatalogDbContext dbContext, ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        logger.LogDebug($"Found product with id {query.Id} converting product to DTO");

        return new GetProductByIdResult(product.Adapt<ProductDTO>());
    }
}
