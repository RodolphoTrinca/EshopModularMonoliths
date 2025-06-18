using Basket.Data.Repository;
using Microsoft.Extensions.Logging;

namespace Basket.Basket.Features.CreateBasket;

public record CreateBasketCommand(ShoppingCartDTO ShoppingCart) : ICommand<CreateBasketResult>;

public record CreateBasketResult(Guid Id);

public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

internal class CreateBasketHandler(
    IBasketRepository repository, 
    ILogger<CreateBasketHandler> logger) 
    : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = CreateNewBasket(command.ShoppingCart);
        logger.LogDebug("Basket created");

        logger.LogDebug($"Saving changes: {shoppingCart}");
        await repository.CreateBasketAsync(shoppingCart, cancellationToken);

        return new CreateBasketResult(shoppingCart.Id);
    }

    private ShoppingCart CreateNewBasket(ShoppingCartDTO shoppingCart)
    {
        var cart = ShoppingCart.Create(
            Guid.NewGuid(),
            shoppingCart.UserName
        );

        foreach (var item in shoppingCart.Items)
        {
            cart.AddItem(item.ProductId, item.Quantity, item.Color, item.Price, item.ProductName);
        }

        return cart;
    }
}
