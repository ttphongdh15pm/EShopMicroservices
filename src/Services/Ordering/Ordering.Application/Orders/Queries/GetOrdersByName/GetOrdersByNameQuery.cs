namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public record GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameQueryResult>;
    public record GetOrdersByNameQueryResult(IEnumerable<OrderDto> Orders);

    public sealed class GetOrderByNameQueryValidator : AbstractValidator<GetOrdersByNameQuery>
    {
        public GetOrderByNameQueryValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Order name is required");
        }
    }
}
