
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shary.Core.Entities;

namespace Shary.Repository.Data.Config;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Properties
        builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
        builder.Property(P => P.Price).IsRequired().HasColumnType("decimal(12,2)");
        builder.Property(P => P.Description).IsRequired();
        builder.Property(P => P.PictureUrl).IsRequired();

        // Relationships
        builder.HasOne(P => P.Category).WithMany().HasForeignKey(P => P.CategoryId);
        builder.HasOne(P => P.Brand).WithMany().HasForeignKey(P => P.BrandId);
    }
}
