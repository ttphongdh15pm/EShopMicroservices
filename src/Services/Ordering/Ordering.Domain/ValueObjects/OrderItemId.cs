namespace Ordering.Domain.ValueObjects
{
    public record OrderItemId
    {
        public Guid Value { get; }

        private OrderItemId(Guid id) => Value = id;

        public static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("OrderItemId cannot be empty");
            }
            return new OrderItemId(value);
        }
    }
}
