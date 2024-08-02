using Basket.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string Username);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", StoreBasketAsync);
        }
        private async Task<IResult> StoreBasketAsync([FromBody] StoreBasketRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<StoreBasketCommand>();
            var result = await sender.Send(command, cancellationToken);
            return Results.Ok(result.Adapt<StoreBasketResponse>());
        }
    }
}
