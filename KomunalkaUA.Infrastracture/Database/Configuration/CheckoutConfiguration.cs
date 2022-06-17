using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class CheckoutConfiguration:IEntityTypeConfiguration<Checkout>
{
    public void Configure(EntityTypeBuilder<Checkout> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.FlatMeter)
            .WithMany(x => x.Checkouts)
            .HasForeignKey(x => x.FlatMeterId);
    }
}