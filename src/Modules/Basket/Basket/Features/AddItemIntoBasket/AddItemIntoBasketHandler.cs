﻿using Catalog.Contracts.Products.Features.GetProductById;

namespace Basket.Basket.Features.AddItemIntoBasket;

public record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDTO ShoppingCartItem)
    : ICommand<AddItemIntoBasketResult>;

public record AddItemIntoBasketResult(Guid Id);

public class AddItemIntoBasketCommandValidator : AbstractValidator<AddItemIntoBasketCommand>
{
    public AddItemIntoBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required.");
        RuleFor(x => x.ShoppingCartItem.ProductId).NotEmpty().WithMessage("Product ID is required.");
        RuleFor(x => x.ShoppingCartItem.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}

internal class AddItemIntoBasketHandler(IBasketRepository repository, ISender sender) 
    : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
{
    public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await repository.GetBasketAsync(command.UserName, false, cancellationToken);

        var result = await sender.Send(
            new GetProductByIdQuery(command.ShoppingCartItem.ProductId));

        shoppingCart.AddItem(
            command.ShoppingCartItem.ProductId,
            command.ShoppingCartItem.Quantity,
            command.ShoppingCartItem.Color,
            result.Product.Price,
            result.Product.Name);

        await repository.SaveChangesAsync(command.UserName, cancellationToken);

        return new AddItemIntoBasketResult(shoppingCart.Id);
    }
}
