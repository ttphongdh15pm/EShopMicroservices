using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.Api.Enpoints
{
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByNameEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{name}", GetOrdersByNameAsync)
                .WithName("GetOrdersByName")
                .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Orders By Name")
                .WithDescription("Get Orders By Name");
        }

        private async Task<IResult> GetOrdersByNameAsync(string name, ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetOrdersByNameQuery(name), cancellationToken);
            var response = result.Adapt<GetOrdersByNameResponse>();
            return Results.Ok(response);
        }
    }
}
