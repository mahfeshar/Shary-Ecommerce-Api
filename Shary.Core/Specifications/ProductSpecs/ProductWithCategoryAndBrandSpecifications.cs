
using Shary.Core.Entities;

namespace Shary.Core.Specifications.ProductSpecs;

public class ProductWithCategoryAndBrandSpecifications : BaseSpecifications<Product>
{
    public ProductWithCategoryAndBrandSpecifications(ProductSpecParams specParams)
        : base(P => (string.IsNullOrEmpty(specParams.search) || P.Name.ToLower().Contains(specParams.search)) 
                            && 
                            (!specParams.categoryId.HasValue || P.CategoryId == specParams.categoryId.Value)
                            &&
                            (!specParams.brandId.HasValue || P.BrandId == specParams.brandId.Value)
              )
    {
        AddIncludesToList();
        if (!string.IsNullOrEmpty(specParams.sort))
        {
            switch (specParams.sort)
            {
                case "priceAsc":
                    AddOrderBy(P => P.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(P => P.Price);
                    break;
                default:
                    AddOrderBy(P => P.Name);
                    break;
            }
        }
        else
            AddOrderBy(P => P.Name);

        ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
    }
    public ProductWithCategoryAndBrandSpecifications(int id)
        : base(P => P.Id == id)
    {
        AddIncludesToList();
    }
    private void AddIncludesToList()
    {
        Includes.Add(P => P.Brand);
        Includes.Add(P => P.Category);
    }
}
