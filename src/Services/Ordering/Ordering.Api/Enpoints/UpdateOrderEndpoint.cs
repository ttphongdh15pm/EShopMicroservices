using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.Api.Enpoints
{
    public record UpdateOrderRequest(OrderDto Order);
    public record UpdateOrderResponse(bool IsSuccess);
    public class UpdateOrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", UpdateOrderAsync)
                .WithName("UpdateOrder")
                .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Order")
                .WithDescription("Update Order");
        }

        private async Task<IResult> UpdateOrderAsync(UpdateOrderRequest request, ISender sender)
        {
            var command = request.Adapt<UpdateOrderCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateOrderResponse>();
            return Results.Ok(response);
        }
    }

}
