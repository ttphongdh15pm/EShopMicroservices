using Basket.Api.Data;
using Basket.Api.Models;
using Discount.Grpc;

namespace Basket.Api.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string Username);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Card can not be null");
            RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("Username is required");
        }
    }

    internal class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountServiceClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            await DeductDiscountAsync(request.Cart, cancellationToken);
            var result = await repository.StoreBasketAsync(request.Cart, cancellationToken);
            return new StoreBasketResult(result.Username);
        }

        private async Task DeductDiscountAsync(ShoppingCart cart, CancellationToken cancellationToken)
        {
            var tasks = cart.Items.Select(item => DeductDiscountAsync(item, cancellationToken)).ToArray();
            await Task.WhenAll(tasks);
        }

        private async Task DeductDiscountAsync(ShoppingCartItem cartItem, CancellationToken cancellationToken)
        {
            var coupon = await discountServiceClient.GetDiscountAsync(new GetDiscountRequest
            {
                ProductName = cartItem.ProductName
            }, cancellationToken: cancellationToken);
            cartItem.Price -= coupon.Amount;
        }
    }
}
