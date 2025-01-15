
using Shary.Core.Entities;
using Shary.Core.Specifications;

namespace Shary.Core.Services.Contract;

public interface IProductService
{
    Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams);
    Task<Product?> GetProductAsync(int productId);
    Task<int> GetCountAsync(ProductSpecParams specParams);
    Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
    Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();
}
