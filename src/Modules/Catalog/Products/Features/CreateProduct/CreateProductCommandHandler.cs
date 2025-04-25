using MediatR;

namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(string name, List<string> category, string description, string imageFile, decimal price) 
    : IRequest<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
