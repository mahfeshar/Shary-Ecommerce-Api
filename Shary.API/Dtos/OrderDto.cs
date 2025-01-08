using Shary.Core.Entities.Order_Aggregate;
using System.ComponentModel.DataAnnotations;

namespace Shary.API.Dtos;

public class OrderDto
{
    [Required]
    public string BuyerEmail { get; set; }
    [Required]
    public string BasketId { get; set; }
    [Required]
    public int DeliveryMethodId { get; set; } // Default 0 : Required - not working
    public AddressDto ShippingAddress { get; set; }
}
