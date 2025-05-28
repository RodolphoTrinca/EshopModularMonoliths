namespace Basket.Basket.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketCommand(string UserName, Guid ProductId)
    : ICommand<RemoveItemFromBasketResult>;

public record RemoveItemFromBasketResult(Guid id);

public class RemoveItemFromBasketCommandValidator : AbstractValidator<RemoveItemFromBasketCommand>
{
    public RemoveItemFromBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required.");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required.");
    }
}

internal class RemoveItemFromBasketHandler(BasketDbContext dbContext)
    : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
{
    public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await dbContext.ShoppingCarts
            .Include(b => b.Items)
            .SingleOrDefaultAsync(b => b.UserName == command.UserName, cancellationToken);

        if (basket == null)
        {
            throw new BaskedNotFoundException(command.UserName);
        }

        basket.RemoveItem(command.ProductId);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new RemoveItemFromBasketResult(basket.Id);
    }
}
