namespace Basket.Api.Basket.CheckoutBasket
{
    public record CheckoutBasketRequest(BasketCheckoutDto Basket);

    public record CheckoutBasketResponse(bool IsSuccess);

    public class CheckoutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", CheckoutBasketAsync)
                .WithName("CheckoutBasket")
                .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Checkout Basket")
                .WithDescription("Checkout Basket");
        }

        private async Task<IResult> CheckoutBasketAsync(CheckoutBasketRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CheckoutBasketCommand>();
            var result = await sender.Send(command, cancellationToken);
            return Results.Ok(result.Adapt<CheckoutBasketResponse>());
        }
    }
}
