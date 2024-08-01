namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Categories, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", CreateProductAsync).WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        }

        private async Task<IResult> CreateProductAsync(ISender sender, CreateProductRequest request, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command, cancellationToken);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        }
    }
}
