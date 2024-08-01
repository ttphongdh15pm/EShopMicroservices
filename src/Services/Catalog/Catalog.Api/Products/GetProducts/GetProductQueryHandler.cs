using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    public class GetProductQueryHandler(IDocumentSession document) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var query = document.Query<Product>();
            var products = await query.ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);
            return new GetProductsResult(products);
        }
    }
}
