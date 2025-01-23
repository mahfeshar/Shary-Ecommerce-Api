using Shary.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Shary.API.Dtos;

public class CustomerBasketDto
{
    [Required]
    public string Id { get; set; }
    [Required]
    public List<BasketItemDto> Items { get; set; }
}
