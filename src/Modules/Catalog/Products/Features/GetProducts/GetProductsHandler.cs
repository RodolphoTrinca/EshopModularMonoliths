using Catalog.Products.Features.GetProductByCategory;
using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery(PaginationRequest PaginationRequest)
    : IQuery<GetProductsResult>;

public record GetProductsResult(PaginatedResult<ProductDTO> Products);

internal class GetProductsHandler(CatalogDbContext dbContext, ILogger<GetProductsHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Products.LongCountAsync(cancellationToken);



        var products = await dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        logger.LogDebug($"Fetched {products.Count} products from the database, converting them to DTOs");

        var productDtos = products.Adapt<List<ProductDTO>>();

        return new GetProductsResult(new PaginatedResult<ProductDTO>(
            pageIndex,
            pageSize,
            totalCount,
            productDtos)
        );
    }
}
