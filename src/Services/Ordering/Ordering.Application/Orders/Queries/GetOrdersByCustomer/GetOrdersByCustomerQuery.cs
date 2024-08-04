namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public record GetOrdersByCustomerQuery(Guid CustomerId): IQuery<GetOrdersByCustomerQueryResult>;
    public record GetOrdersByCustomerQueryResult(IEnumerable<OrderDto> Orders);

    public class GetOrdersByCustomerQueryValidator : AbstractValidator<GetOrdersByCustomerQuery>
    {
        public GetOrdersByCustomerQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().NotNull().WithMessage("CustomerId is required");
        }
    }
}
