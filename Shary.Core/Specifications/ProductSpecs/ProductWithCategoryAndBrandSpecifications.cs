
using Shary.Core.Entities;

namespace Shary.Core.Specifications.ProductSpecs;

public class ProductWithCategoryAndBrandSpecifications : BaseSpecifications<Product>
{
    public ProductWithCategoryAndBrandSpecifications()
        : base()
    {
        AddIncludesToList();
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
