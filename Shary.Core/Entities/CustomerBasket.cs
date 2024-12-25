
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
}
