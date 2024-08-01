namespace Catalog.Api.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", UpdateProductAsync)
                .WithName("UpdateProduct")
                .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Product")
                .WithDescription("Update Product");
        }
        private async Task<IResult> UpdateProductAsync(UpdateProductRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command, cancellationToken);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        }
}
}
