
namespace Shary.Core.Entities.Order_Aggregate;

public class OrderItem : BaseEntity
{
    public OrderItem()
    {
        
    }
    public OrderItem(ProductItemOrdered product, decimal price, int quantity)
    {
        Product = product;
        Price = price;
        Quantity = quantity;
    }

    public ProductItemOrdered Product { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
