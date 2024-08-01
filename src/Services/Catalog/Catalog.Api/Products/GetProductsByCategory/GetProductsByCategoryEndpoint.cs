namespace Catalog.Api.Products.GetProductsByCategory
{

    public record GetProductByCategoryRequest(string Category);
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", GetProductsByCategoryAsync)
                .WithName("GetProductByCategory")
                .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product By Category")
                .WithDescription("Get Product By Category");
        }

        private async Task<IResult> GetProductsByCategoryAsync([AsParameters] GetProductByCategoryRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request.Adapt<GetProductByCategoryQuery>(), cancellationToken);
            var response = result.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(response);
        }
    }
}
