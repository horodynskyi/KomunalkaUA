using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class FlatConfiguration:IEntityTypeConfiguration<Flat>
{
    public void Configure(EntityTypeBuilder<Flat> builder)
    {
        builder
            .Property(x => x.Id);
        builder
            .HasOne(x => x.Owner)
            .WithMany(x => x.Owners)
            .HasForeignKey(x => x.OwnerId);
        builder
            .HasOne(x => x.Tenant)
            .WithMany(x => x.Tenants)
            .HasForeignKey(x => x.TenantId);
        builder
            .HasOne(x => x.ElectricMeter)
            .WithMany(x => x.ElectricMeters)
            .HasForeignKey(x => x.ElectricMeterId);
        builder
            .HasOne(x => x.GasMeter)
            .WithMany(x => x.GasMeters)
            .HasForeignKey(x => x.GasMeterId);
        builder
            .HasOne(x => x.WatterMeter)
            .WithMany(x => x.WaterMeter)
            .HasForeignKey(x => x.WatterMeterId);
        builder
            .HasOne(x => x.Address)
            .WithMany(x => x.Flats)
            .HasForeignKey(x => x.AddressId);
        builder
            .Ignore(x => x.ElectricMeter); 
        builder
            .Ignore(x => x.WatterMeter);
        builder
            .Ignore(x => x.GasMeter);

    }
}