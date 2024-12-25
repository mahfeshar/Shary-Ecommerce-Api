﻿
using Shary.Core.Entities;
using Shary.Core.Repositories.Contract;
using StackExchange.Redis;
using System.Text.Json;

namespace Shary.Repository;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }
    public async Task<CustomerBasket?> GetBasketAsync(string basketId)
    {
        RedisValue basket = await _database.StringGetAsync(basketId);
        return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
    }
    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
    {
        bool createdOrUpdated = await _database.StringSetAsync(
            basket.Id,
            JsonSerializer.Serialize(basket),
            TimeSpan.FromDays(30)
            );
        if (!createdOrUpdated) return null;
        return await GetBasketAsync(basket.Id);
    }
    public async Task<bool> DeleteBasketAsync(string BasketId)
    {
        return await _database.KeyDeleteAsync(BasketId);
    }
}
