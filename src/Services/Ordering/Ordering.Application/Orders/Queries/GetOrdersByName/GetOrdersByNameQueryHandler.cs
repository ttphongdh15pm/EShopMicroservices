namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    internal class GetOrdersByNameQueryHandler(IOrderingDataContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameQueryResult>
    {
        public async Task<GetOrdersByNameQueryResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
                 .AsNoTracking()
                 .Where(o => o.OrderName.Value.Contains(query.Name))
                 .OrderBy(o => o.OrderName.Value)
                 .ToListAsync(cancellationToken);

            return new GetOrdersByNameQueryResult(orders.ToOrderDtoList());
        }
    }
}
