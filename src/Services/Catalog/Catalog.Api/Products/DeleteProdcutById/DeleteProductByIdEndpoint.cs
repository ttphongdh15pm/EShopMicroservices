namespace Catalog.Api.Products.DeleteProdcutById
{
    public record DeleteProductByIdRequest(Guid Id);
    public record DeleteProductByIdResponse(bool IsSuccess);
    public class DeleteProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", DeleteProductByIdAsync)
            .WithName("DeleteProduct")
            .Produces<DeleteProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }

        private async Task<IResult> DeleteProductByIdAsync([AsParameters] DeleteProductByIdRequest request, ISender sender)
        {
            var result = await sender.Send(request.Adapt<DeleteProductByIdCommand>());
            var response = result.Adapt<DeleteProductByIdResponse>();
            return Results.Ok(response);
        }
    }
}
