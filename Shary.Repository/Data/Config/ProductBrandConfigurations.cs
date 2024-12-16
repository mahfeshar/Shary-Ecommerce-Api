
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shary.Core.Entities;

namespace Shary.Repository.Data.Config;

public class ProductBrandConfigurations : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
    }
}
