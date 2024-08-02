using Basket.Api.Data;
using Basket.Api.Models;

namespace Basket.Api.Basket.GetBasket
{
    public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);

    internal sealed class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasketAsync(request.Username);
            return new GetBasketResult(basket);
        }
    }
}
