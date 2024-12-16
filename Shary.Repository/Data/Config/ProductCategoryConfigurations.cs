
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shary.Core.Entities;

namespace Shary.Repository.Data.Config;

public class ProductCategoryConfigurations : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
    }
}
