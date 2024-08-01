namespace Catalog.Api.Products.GetProductById
{
    public record GetProductByIdRequest(Guid Id);
    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", GetProductByIdAsync)
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
        }

        private async Task<IResult> GetProductByIdAsync([AsParameters] GetProductByIdRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var query = request.Adapt<GetProductByIdQuery>();
            var result = await sender.Send(query, cancellationToken);
            if(result.Product is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(result.Adapt<GetProductByIdResponse>());
        }
    }
}
