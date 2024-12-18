using Shary.Core.Entities;

namespace Shary.API.Dtos;

public class ProductToReturnDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }
    public string Category { get; set; }
    public int CategoryId { get; set; }
    public string Brand { get; set; }
    public int BrandId { get; set; }
}
