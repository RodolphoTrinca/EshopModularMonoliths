namespace Catalog.Products.Features.GetProductByCategory;

//public record GetProductByCategoryRequest(string categoryName);

public record GetProductByCategoryResponse(IEnumerable<ProductDTO> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{categoryName}", async (string categoryName, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByCategoryQuery(categoryName));
            
            var response = result.Adapt<GetProductByCategoryResponse>();
            
            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Get products by category")
        .WithDescription("Get products by category");
    }
}
