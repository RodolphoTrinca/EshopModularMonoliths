using Microsoft.Extensions.Logging;

namespace Basket.Basket.Features.GetBasket;

public record GetBasketQuery(string UserName)
    : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCartDTO ShoppingCart);

internal class GetBasketHandler(
    BasketDbContext dbContext,
    ILogger<GetBasketHandler> logger) 
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
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

        return new GetBasketResult(basketDto);
    }
}
