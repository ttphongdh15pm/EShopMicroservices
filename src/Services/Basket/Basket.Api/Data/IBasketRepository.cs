using Basket.Api.Models;

namespace Basket.Api.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellationToken = default);
        Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasketAsync(string username, CancellationToken cancellationToken = default);
    }
}
