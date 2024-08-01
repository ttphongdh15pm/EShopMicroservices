namespace Catalog.Api.Products.GetProductsByCategory
{

    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    public class GetProductsByCategoryQueryHandler(IDocumentSession document) : IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await document.Query<Product>()
                .Where(prod => prod.Categories.Contains(request.Category))
                .ToListAsync(cancellationToken);
            return new GetProductByCategoryResult(products);
        }
    }
}
