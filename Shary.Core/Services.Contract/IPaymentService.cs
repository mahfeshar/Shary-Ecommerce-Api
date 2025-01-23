
using Shary.Core.Entities;

namespace Shary.Core.Services.Contract;

public interface IPaymentService
{
    Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
}
