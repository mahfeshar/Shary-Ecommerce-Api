
using Shary.Core.Entities.Order_Aggregate;

namespace Shary.Core.Specifications;

public class OrderSpecifications : BaseSpecifications<Order>
{
    public OrderSpecifications(string buyerEmail)
        :base(O => O.BuyerEmail == buyerEmail)
    {
        ApplyIncludesForOrder();
        AddOrderByDesc(O => O.OrderDate);
    }


    public OrderSpecifications(int orderId, string buyerEmail)
        :base(O => O.Id == orderId && O.BuyerEmail ==  buyerEmail)
    {
        ApplyIncludesForOrder();
    }
    private void ApplyIncludesForOrder()
    {
        Includes.Add(O => O.DeliveryMethod);
        Includes.Add(O => O.Items);
    }
}
