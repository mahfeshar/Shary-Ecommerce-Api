
using Shary.Core.Entities;

namespace Shary.Core.Specifications.ProductSpecs;

public class ProductWithCategoryAndBrandSpecifications : BaseSpecifications<Product>
{
    public ProductWithCategoryAndBrandSpecifications(string? sort)
        : base()
    {
        AddIncludesToList();
        if (!string.IsNullOrEmpty(sort))
        {
            switch (sort)
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
