
using Shary.Core;
using Shary.Core.Entities;
using Shary.Core.Services.Contract;
using Shary.Core.Specifications;
using Shary.Core.Specifications.ProductSpecs;

namespace Shary.Service;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams)
    {
        ProductWithCategoryAndBrandSpecifications? spec = new(specParams);
        IReadOnlyList<Product>? products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
        return products;
    }
    public async Task<Product?> GetProductAsync(int productId)
    {
        ProductWithCategoryAndBrandSpecifications? spec = new ProductWithCategoryAndBrandSpecifications(productId);
        Product? product = await _unitOfWork.Repository<Product>().GetWithSpecAsync(spec);
        return product;
    }
    public async Task<int> GetCountAsync(ProductSpecParams specParams)
    {
        var countSpec = new ProductsWithFiltrationForCountSpec(specParams);
        var count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(countSpec);
        return count;
    }


    public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        => await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

    public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
        => await _unitOfWork.Repository<ProductCategory>().GetAllAsync();
}
