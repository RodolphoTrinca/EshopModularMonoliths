using Basket.Basket.DTOs;
using Basket.Basket.Exceptions;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Basket.Basket.Features.GetBasket;

public record GetBasketQuery(string UserName)
    : IQuery<GetBasketResponse>;

public record GetBasketResponse(ShoppingCartDTO ShoppingCart);

internal class GetBasketHandler(
    BasketDbContext dbContext,
    ILogger<GetBasketHandler> logger) 
    : IQueryHandler<GetBasketQuery, GetBasketResponse>
{
    public async Task<GetBasketResponse> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await dbContext.ShoppingCarts
            .AsNoTracking()
            .Include(x => x.Items)
            .SingleOrDefaultAsync(x => x.UserName == query.UserName, cancellationToken);

        if (basket is null)
        {
            throw new BaskedNotFoundException(query.UserName);
        }

        logger.LogDebug("Basket found!");

        var basketDto = basket.Adapt<ShoppingCartDTO>();

        return new GetBasketResponse(basketDto);
    }
}
