using Catalog.Api.Exceptions;

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
            if (product == null)
            {
                throw new ProductNotFoundException(request.Id);
            }
            return new GetProductByIdResult(product);
        }
    }
}
