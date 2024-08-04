﻿namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler(IOrderingDataContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;
            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

            var orders = await dbContext.Orders
                           .Include(o => o.OrderItems)
                           .OrderBy(o => o.OrderName.Value)
                           .Skip((pageIndex - 1) * pageSize) 
                           .Take(pageSize)
                           .ToListAsync(cancellationToken);

            return new GetOrdersResult(
                new PaginatedResult<OrderDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    orders.ToOrderDtoList()));
        }
    }
}
