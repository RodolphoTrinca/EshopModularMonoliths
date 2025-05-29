using Basket.Data.Repository;
using Microsoft.Extensions.Logging;

namespace Basket.Basket.Features.GetBasket;

public record GetBasketQuery(string UserName)
    : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCartDTO ShoppingCart);

internal class GetBasketHandler(
    IBasketRepository repository,
    ILogger<GetBasketHandler> logger) 
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasketAsync(query.UserName, true, cancellationToken);

        logger.LogDebug("Basket found!");

        var basketDto = basket.Adapt<ShoppingCartDTO>();

        return new GetBasketResult(basketDto);
    }
}
