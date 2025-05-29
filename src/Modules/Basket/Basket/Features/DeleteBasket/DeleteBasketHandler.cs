using Basket.Data.Repository;
using Microsoft.Extensions.Logging;

namespace Basket.Basket.Features.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool Success);

internal class DeleteBasketHandler(
    IBasketRepository repository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteBasketAsync(command.UserName, cancellationToken);

        return new DeleteBasketResult(true);
    }
}
