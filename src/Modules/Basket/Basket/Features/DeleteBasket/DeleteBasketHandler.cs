using Microsoft.Extensions.Logging;

namespace Basket.Basket.Features.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool Success);

internal class DeleteBasketHandler(
    BasketDbContext dbContext,
    ILogger<DeleteBasketHandler> logger)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await dbContext.ShoppingCarts
            .SingleOrDefaultAsync(x => x.UserName == command.UserName);

        if (basket == null)
        {
            throw new BaskedNotFoundException(command.UserName);
        }

        logger.LogDebug($"Deleting basket id: {basket.Id} for user: {command.UserName}");

        dbContext.ShoppingCarts.Remove(basket);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogDebug($"Basked id: {basket.Id} deleted");

        return new DeleteBasketResult(true);
    }
}
