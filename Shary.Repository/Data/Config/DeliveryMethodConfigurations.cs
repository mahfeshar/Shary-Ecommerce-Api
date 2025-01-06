
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shary.Core.Entities.Order_Aggregate;

namespace Shary.Repository.Data.Config;

internal class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(deliveryMethod => deliveryMethod.Cost)
            .HasColumnType("decimal(18,2)");
    }
}
