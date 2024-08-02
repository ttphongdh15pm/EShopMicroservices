using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Basket.DeleteBasket
{
    public record DeleteBasketRequest(string Username);
    public record DeleteBasketResponse(bool IsSuccess);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{Username}", DeleteBasketAsync)
                .WithName("DeleteBasket")
                .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Basket")
                .WithDescription("Delete Basket");
        }

        private async Task<IResult> DeleteBasketAsync([AsParameters] DeleteBasketRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<DeleteBasketCommand>();
            var result = await sender.Send(command, cancellationToken);
            return Results.Ok(result.Adapt<DeleteBasketResponse>());
        }
    }
}
