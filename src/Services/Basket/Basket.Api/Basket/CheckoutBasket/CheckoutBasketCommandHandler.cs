
using Basket.Api.Data;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.Api.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto Basket) : ICommand<CheckoutBasketCommandResult>;

    public record CheckoutBasketCommandResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator
    : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.Basket).NotNull().WithMessage("BasketCheckoutDto can't be null");
            RuleFor(x => x.Basket.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }

    public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketCommandResult>
    {
        public async Task<CheckoutBasketCommandResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasketAsync(command.Basket.UserName, cancellationToken);
            if(basket is null)
            {
                return new CheckoutBasketCommandResult(false);
            }
            var eventMessage = command.Basket.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;
            await publishEndpoint.Publish(eventMessage, cancellationToken);
            await repository.DeleteBasketAsync(command.Basket.UserName, cancellationToken);
            return new CheckoutBasketCommandResult(true);
        }
    }
}
