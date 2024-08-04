using BuildingBlocks.Pagination;
using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetProductsResult>;
    public record GetProductsResult(PaginatedResult<Product> Products);
    public class GetProductQueryHandler(IDocumentSession document) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;
            var queryExpression = document.Query<Product>();
            var totalCount = await queryExpression.LongCountAsync(cancellationToken);
            var products = await queryExpression.ToPagedListAsync(pageIndex, pageSize, cancellationToken);
            return new GetProductsResult(new PaginatedResult<Product>(pageIndex, pageSize, totalCount, products));
        }
    }
}
