using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Api.Enpoints
{
    public record CreateOrderRequest(OrderDto Order);
    public record CreateOrderResponse(Guid Id);
    public class CreateOrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", CreateOrderAsync)
                .WithName("CreateOrder")
                .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Order")
                .WithDescription("Create Order");
        }
        private async Task<IResult> CreateOrderAsync(CreateOrderRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CreateOrderCommand>();
            var result = await sender.Send(command, cancellationToken);
            var response = result.Adapt<CreateOrderResponse>();
            return Results.Created($"/orders/{response.Id}", response);
        }
    }
}
