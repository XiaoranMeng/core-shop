using Core.Entities;
using Core.Intefaces;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _redis;

        public CartRepository(IConnectionMultiplexer multiplexer)
        {
            _redis = multiplexer.GetDatabase();
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            var cart = await _redis.StringGetAsync(cartId);

            return !cart.IsNullOrEmpty
                ? JsonSerializer.Deserialize<Cart>(cart)
                : null;
        }

        public async Task<Cart> CreateOrUpdateCartAsync(Cart cart)
        {
            var isCreated = await _redis.StringSetAsync(
                cart.Id, 
                JsonSerializer.Serialize(cart),
                TimeSpan.FromDays(30));

            if (!isCreated)
            {
                return null;
            }

            return await GetCartAsync(cart.Id);

        }

        public async Task<bool> DeleteCartAsync(string cartId)
        {
            return await _redis.KeyDeleteAsync(cartId);   
        }
    }
}
