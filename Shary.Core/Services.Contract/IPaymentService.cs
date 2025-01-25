
using Shary.Core.Entities;
using Shary.Core.Entities.Order_Aggregate;

namespace Shary.Core.Services.Contract;

public interface IPaymentService
{
    Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
    Task<Order> UpdateIntentToSucceededOrFailed(string paymentIntentId, bool isSucceeded);
}
