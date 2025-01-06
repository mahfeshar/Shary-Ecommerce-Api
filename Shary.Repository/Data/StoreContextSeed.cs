
using Shary.Core.Entities;
using Shary.Core.Entities.Order_Aggregate;
using System.Text.Json;

namespace Shary.Repository.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext _dbContext)
    {
        if (!_dbContext.Brands.Any())
        {
            string? brandsData = File.ReadAllText("../Shary.Repository/Data/DataSeeding/brands.json");
            List<ProductBrand>? brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if (brands?.Count > 0)
            {
                foreach (ProductBrand brand in brands)
                {
                    _dbContext.Brands.Add(brand);
                }
            }
        }
        if (!_dbContext.Categories.Any())
        {
            string? categoriesData = File.ReadAllText("../Shary.Repository/Data/DataSeeding/categories.json");
            List<ProductCategory>? categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
            if (categories?.Count > 0)
            {
                foreach (ProductCategory category in categories)
                {
                    _dbContext.Categories.Add(category);
                }
            }
        }
        if (!_dbContext.Products.Any())
        {
            string? productsData = File.ReadAllText("../Shary.Repository/Data/DataSeeding/products.json");
            List<Product>? products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products?.Count > 0)
            {
                foreach (Product product in products)
                {
                    _dbContext.Products.Add(product);
                }
            }
        }
        if(!_dbContext.DeliveryMethods.Any())
        {
            string? deliveryMethodsData = File.ReadAllText("../Shary.Repository/Data/DataSeeding/delivery.json");
            var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
            if(deliveryMethods?.Count > 0)
            {
                foreach (var deliveryMethod in deliveryMethods)
                {
                    _dbContext.Set<DeliveryMethod>().Add(deliveryMethod);
                }
            }
        }
        await _dbContext.SaveChangesAsync();
    }
}
