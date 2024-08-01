namespace Catalog.Api.Products.GetProductById
{

    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product? Product);
    public class GetProductByIdQueryHandler(IDocumentSession document) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await document.Query<Product>()
                .Where(prod => prod.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            return new GetProductByIdResult(product);
        }
    }
}
