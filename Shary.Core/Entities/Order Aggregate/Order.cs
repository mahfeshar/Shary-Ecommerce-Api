
namespace Shary.Core.Entities.Order_Aggregate;

public class Order : BaseEntity
{
    public Order()
    {
        
    }
    public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal)
    {
        BuyerEmail = buyerEmail;
        ShippingAddress = shippingAddress;
        DeliveryMethod = deliveryMethod;
        Items = items;
        Subtotal = subtotal;
    }

    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public Address ShippingAddress { get; set; }
    public DeliveryMethod? DeliveryMethod { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
    public decimal Subtotal { get; set; }
    public decimal Total() => Subtotal + DeliveryMethod.Cost;
    public string PaymentIntentId { get; set; } = string.Empty;
}
