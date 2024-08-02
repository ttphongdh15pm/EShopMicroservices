using Basket.Api.Models;

namespace Basket.Api.Basket.GetBasket
{
    public record GetBasketRequest(string Username);
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{Username}", GetBasketAsync)
                .WithName("GetBasket")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Basket")
                .WithDescription("Get Basket");
        }

        private async Task<IResult> GetBasketAsync([AsParameters] GetBasketRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var query = request.Adapt<GetBasketQuery>();
            var result = await sender.Send(query, cancellationToken);
            return Results.Ok(result.Adapt<GetBasketResponse>());
        }
    }
}
