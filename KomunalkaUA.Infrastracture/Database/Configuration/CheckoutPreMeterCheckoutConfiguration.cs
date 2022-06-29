using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class CheckoutPreMeterCheckoutConfiguration:IEntityTypeConfiguration<CheckoutPreMeterCheckout>
{
    public void Configure(EntityTypeBuilder<CheckoutPreMeterCheckout> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.Checkout)
            .WithMany(x => x.CheckoutPreMeterCheckouts)
            .HasForeignKey(x => x.CheckoutId);
        builder
            .HasOne(x => x.PreMeterCheckout)
            .WithMany(x => x.CheckoutPreMeterCheckouts)
            .HasForeignKey(x => x.PreMeterCheckoutId);
    }
}