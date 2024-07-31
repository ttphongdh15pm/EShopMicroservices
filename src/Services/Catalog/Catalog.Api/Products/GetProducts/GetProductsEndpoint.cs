using Catalog.Api.Models;

namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsResponse(int Total, List<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", GetProductsAsync);
        }

        private async Task<IResult> GetProductsAsync(ISender sender, CancellationToken cancellationToken)
        {
            var getProductsQuery = new GetProductsQuery();
            var result = await sender.Send(getProductsQuery, cancellationToken);
            return Results.Ok(result.Adapt<GetProductsResponse>());
        }
    }
}
