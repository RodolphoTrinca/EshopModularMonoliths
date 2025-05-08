
namespace Catalog.Products.Features.GetProducts;

//public record GetProductsRequest();

public record GetProductsResponse(IEnumerable<ProductDTO> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            
            var response = result.Adapt<GetProductsResponse>();
            
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .WithTags("Products")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Get all products")
        .WithDescription("Get all products from the catalog");
    }
}
