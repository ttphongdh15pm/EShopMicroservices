using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.Api.Enpoints
{
    public record DeleteOrderRequest(Guid Id);
    public record DeleteOrderResponse(bool IsSuccess);
    public class DeleteOrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{orderId}", DeleteOrderAsync)
                .WithName("DeleteOrder")
                .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Order")
                .WithDescription("Delete Order");
        }
        private async Task<IResult> DeleteOrderAsync(Guid orderId, ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new DeleteOrderCommand(orderId), cancellationToken);
            var response = result.Adapt<DeleteOrderResponse>();
            return Results.Ok(response);
        }
    }
}
