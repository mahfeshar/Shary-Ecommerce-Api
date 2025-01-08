
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shary.Core.Entities.Order_Aggregate;

namespace Shary.Repository.Data.Config;

internal class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(O => O.ShippingAddress,
            shippingAddress => shippingAddress.WithOwner());
        builder.Property(O => O.Status)
            .HasConversion(
                OStatus => OStatus.ToString(),
                OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
            );
        builder.HasOne(O => O.DeliveryMethod)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);
        builder.Property(O => O.Subtotal)
            .HasColumnType("decimal(18,2)");

    }
}
