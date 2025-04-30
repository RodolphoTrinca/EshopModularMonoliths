namespace Catalog.Products.Features.GetProducts;

public record GetProductsCommand()
    : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<ProductDTO> Products);

internal class GetProductsHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsCommand, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsCommand query, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDtos = products.Adapt<List<ProductDTO>>();

        return new GetProductsResult(productDtos);
    }
}
