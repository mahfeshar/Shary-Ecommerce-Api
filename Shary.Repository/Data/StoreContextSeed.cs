
using Shary.Core.Entities;
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
            List<ProductCategory>? categoreis = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
            if (categoreis?.Count > 0)
            {
                foreach (ProductCategory category in categoreis)
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
        await _dbContext.SaveChangesAsync();
    }
}
