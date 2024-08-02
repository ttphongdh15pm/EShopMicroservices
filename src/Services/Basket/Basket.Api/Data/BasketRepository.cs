using Basket.Api.Exceptions;
using Basket.Api.Models;

namespace Basket.Api.Data
{
    public class BasketRepository(IDocumentSession document) : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string username, CancellationToken cancellationToken = default)
        {
            document.Delete<ShoppingCart>(username);
            await document.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellationToken = default)
        {
            var basket = await document.LoadAsync<ShoppingCart>(username, cancellationToken);
            if(basket is null)
            {
                throw new BasketNotFoundException(username);
            }
            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            document.Store<ShoppingCart>(basket);
            await document.SaveChangesAsync(cancellationToken);
            return basket;
        }
    }
}
