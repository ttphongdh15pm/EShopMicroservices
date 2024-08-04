using BuildingBlocks.Pagination;

namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsRequest(PaginationRequest PaginationRequest);
    public record GetProductsResponse(PaginatedResult<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", GetProductsAsync);
        }

        private async Task<IResult> GetProductsAsync(ISender sender, [AsParameters] GetProductsRequest request, CancellationToken cancellationToken)
        {
            var query = request.Adapt<GetProductsQuery>();
            var result = await sender.Send(query, cancellationToken);
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        }
    }
}
