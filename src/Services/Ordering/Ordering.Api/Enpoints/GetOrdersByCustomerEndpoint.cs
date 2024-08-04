﻿using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.Api.Enpoints
{
    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

    public class GetOrdersByCustomerEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", GetOrdersByCustomerAsync)
                .WithName("GetOrdersByCustomer")
                .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Orders By Customer")
                .WithDescription("Get Orders By Customer");
        }

        private async Task<IResult> GetOrdersByCustomerAsync(Guid customerId, ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetOrdersByCustomerQuery(customerId), cancellationToken);
            var response = result.Adapt<GetOrdersByCustomerResponse>();
            return Results.Ok(response);
        }
    }
}
