
using Shary.Core.Entities;

namespace Shary.Core.Repositories.Contract;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(string BasketId);
    Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(string BasketId);
}
