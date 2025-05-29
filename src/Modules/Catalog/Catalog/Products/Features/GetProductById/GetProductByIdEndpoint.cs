using Catalog.Contracts.Products.Features.GetProductById;

namespace Catalog.Products.Features.GetProductById;

//public record GetProductByIdRequest(Guid id);

public record GetProductByIdResponse(ProductDTO Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async(Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));

            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        //.Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Get a product by ID")
        .WithDescription("Get a product by ID");
    }
}
