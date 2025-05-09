namespace Catalog.Products.Features.DeleteProduct;

public record DeleteProductCommand(Guid ProductId)
    : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required");
    }
}

internal class DeleteProductHandler(CatalogDbContext dbContext, ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogDebug($"Finding product with ID: {command.ProductId}");

        var product = await dbContext.Products
            .FindAsync([command.ProductId], cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.ProductId);
        }

        logger.LogDebug($"Product found: {product} deleting from database");
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogDebug($"Product with ID: {command.ProductId} deleted successfully");

        return new DeleteProductResult(true);
    }
}
