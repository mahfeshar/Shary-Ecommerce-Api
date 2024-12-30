
using Shary.Core.Entities;

namespace Shary.Core.Specifications.ProductSpecs;

public class ProductsWithFiltrationForCountSpec : BaseSpecifications<Product>
{
    public ProductsWithFiltrationForCountSpec(ProductSpecParams specParams)
        :base(P => 
            (string.IsNullOrEmpty(specParams.search) || P.Name.ToLower().Contains(specParams.search)) &&
            (!specParams.brandId.HasValue || P.BrandId == specParams.brandId) &&
            (!specParams.categoryId.HasValue || P.CategoryId == specParams.categoryId))
    {
        
    }
}
