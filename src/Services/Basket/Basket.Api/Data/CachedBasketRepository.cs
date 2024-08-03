using Basket.Api.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string username, CancellationToken cancellationToken = default)
        {
            var removingCachedBasketTask = cache.RemoveAsync(username, cancellationToken);
            var removingBasketTask = repository.DeleteBasketAsync(username, cancellationToken);
            await Task.WhenAll(removingCachedBasketTask, removingBasketTask);
            return true;
        }

        public async Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(username, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }
            var basket = await repository.GetBasketAsync(username, cancellationToken);
            await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            var storeBasketTask = repository.StoreBasketAsync(basket, cancellationToken);
            var cachingBasketTask = cache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket), cancellationToken);
            await Task.WhenAll(storeBasketTask, cachingBasketTask);
            return basket;
        }
    }
}
