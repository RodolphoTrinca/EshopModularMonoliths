using Ordering.Orders.DTOs;

namespace Ordering.Orders.Features.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest)
    : IQuery<GetOrdersResult>;
public record GetOrdersResult(PaginatedResult<OrderDTO> Orders);

internal class GetOrdersHandler(OrderingDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders
                        .AsNoTracking()
                        .Include(x => x.Items)
                        .OrderBy(p => p.OrderName)
                        .Skip(pageSize * pageIndex)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken);

        var orderDtos = orders.Adapt<List<OrderDTO>>();
        return new GetOrdersResult(
            new PaginatedResult<OrderDTO>(
                pageIndex,
                pageSize,
                totalCount,
                orderDtos));
    }
}