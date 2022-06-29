using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class PreMeterCheckoutConfiguration:IEntityTypeConfiguration<PreMeterCheckout>
{
    public void Configure(EntityTypeBuilder<PreMeterCheckout> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.Flat)
            .WithMany(x => x.PreMeterCheckouts)
            .HasForeignKey(x => x.FlatId);
        builder
            .HasOne(x => x.Meter)
            .WithMany(x => x.PreMeterCheckouts)
            .HasForeignKey(x => x.MeterId);
        builder
            .HasOne(x => x.Tenant)
            .WithMany(x => x.PreMeterCheckouts)
            .HasForeignKey(x => x.TenantId);
    }
}