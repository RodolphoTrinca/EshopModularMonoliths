﻿using Ordering.Orders.DTOs;

namespace Ordering.Orders.Features.GetOrderById;

public record GetOrderByIdQuery(Guid Id)
    : IQuery<GetOrderByIdResult>;
public record GetOrderByIdResult(OrderDTO Order);

internal class GetOrderByIdHandler(OrderingDbContext dbContext)
    : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult>
{
    public async Task<GetOrderByIdResult> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
                        .AsNoTracking()
                        .Include(x => x.Items)
                        .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(query.Id);
        }

        var orderDto = order.Adapt<OrderDTO>();

        return new GetOrderByIdResult(orderDto);
    }
}