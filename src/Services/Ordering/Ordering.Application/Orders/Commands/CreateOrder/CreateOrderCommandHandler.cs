namespace Ordering.Application.Orders.Commands.CreateOrder
{
    internal class CreateOrderCommandHandler(IOrderingDataContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderCommandResult>
    {
        public async Task<CreateOrderCommandResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = CreateNewOrder(command.Order);
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderCommandResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto order)
        {
            var shippingAddress = CreateAddress(order.ShippingAddress);
            var billingAddress = CreateAddress(order.BillingAddress);
            var payment = CreatePayment(order.Payment);
            return Order.Create(OrderId.Of(order.Id), OrderName.Of(order.OrderName), CustomerId.Of(order.CustomerId), shippingAddress, billingAddress, payment);
        }

        private Address CreateAddress(AddressDto address)
        {
            return Address.Of(address.FirstName, address.LastName, address.EmailAddress, address.AddressLine, address.Country, address.State, address.ZipCode);
        }

        private Payment CreatePayment(PaymentDto payment)
        {
            return Payment.Of(payment.CardName, payment.CardNumber, payment.Expiration, payment.Cvv, payment.PaymentMethod);
        }
    }
}
