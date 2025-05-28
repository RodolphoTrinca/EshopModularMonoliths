﻿namespace Basket.Basket.Features.GetBasket;

//public record GetBasketRequest(string UserName); 
public record GetBasketResponse(ShoppingCartDTO ShoppingCart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));

            var response = result.Adapt<GetBasketResult>();

            return Results.Ok(response);
        })
        .Produces<GetBasketResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket")
        .WithDescription("Get Basket")
        .RequireAuthorization();
    }
}