using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.Api.Enpoints
{
    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

    public class GetOrdersEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{PageIndex}/{PageSize}", GetOrdersAsync)
                .WithName("GetOrders")
                .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Orders")
                .WithDescription("Get Orders");
        }
        private async Task<IResult> GetOrdersAsync(int PageIndex, int PageSize, ISender sender, CancellationToken cancellation)
        {
            var result = await sender.Send(new GetOrdersQuery(new PaginationRequest(PageIndex, PageSize)), cancellation);
            var response = result.Adapt<GetOrdersResponse>();
            return Results.Ok(response);
        }
    }
}
