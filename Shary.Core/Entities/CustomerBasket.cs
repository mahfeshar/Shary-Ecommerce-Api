
namespace Shary.Core.Entities;

public class CustomerBasket
{
    public CustomerBasket()
    {
    }
    public CustomerBasket(string basketId)
    {
        Id = basketId;
        Items = new List<BasketItem>();
    }

    public string Id { get; set; }
    public List<BasketItem> Items { get; set; }
    public int? DeliveryMethodId { get; set; }
    public decimal ShippingPrice { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
}
