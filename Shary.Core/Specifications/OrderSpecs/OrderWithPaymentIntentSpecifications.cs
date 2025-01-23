
using Shary.Core.Entities.Order_Aggregate;

namespace Shary.Core.Specifications.OrderSpecs;

public class OrderWithPaymentIntentSpecifications : BaseSpecifications<Order>
{
    public OrderWithPaymentIntentSpecifications(string paymentIntentId)
        :base(O => O.PaymentIntentId == paymentIntentId)
    {
        
    }
}
