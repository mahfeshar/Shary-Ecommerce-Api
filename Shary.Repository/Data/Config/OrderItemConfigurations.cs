
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shary.Core.Entities.Order_Aggregate;

namespace Shary.Repository.Data.Config;

internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.OwnsOne(orderItem => orderItem.Product,
            Product => Product.WithOwner());
        builder.Property(orderItem => orderItem.Price)
            .HasColumnType("decimal(18,2)");
    }
}
