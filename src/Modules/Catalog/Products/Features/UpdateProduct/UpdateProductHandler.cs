namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDTO Product)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

internal class UpdateProductHandler(CatalogDbContext dbContext, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogDebug($"Searching for the product with Id {command.Product.Id}");

        var product = await dbContext.Products
            .FindAsync([command.Product.Id], cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Product.Id);
        }

        logger.LogDebug($"Updating entity of the product with Id {command.Product.Id} - orignal product: {product} / update product: {command.Product}");

        UpdateProductWithNewValues(product, command.Product);

        logger.LogDebug($"Saving changes to the product with Id {command.Product.Id}");

        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogDebug($"Product with Id {command.Product.Id} updated successfully");

        return new UpdateProductResult(true);
    }

    private void UpdateProductWithNewValues(Product product, ProductDTO productDTO)
    {
        product.Update(
            productDTO.Name,
            productDTO.Category,
            productDTO.Description,
            productDTO.ImageFile,
            productDTO.Price
        );
    }
}
