﻿namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDTO Product)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

internal class UpdateProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FindAsync([command.Product.Id], cancellationToken);

        if (product is null)
        {
            throw new Exception($"Product with ID {command.Product.Id} not found.");
        }

        UpdateProductWithNewValues(product, command.Product);

        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);

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
