using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class FlatMeterConfiguration:IEntityTypeConfiguration<FlatMeter>
{
    
    public void Configure(EntityTypeBuilder<FlatMeter> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.Flat)
            .WithMany(x => x.FlatMeters)
            .HasForeignKey(x => x.FlatId);
        builder
            .HasOne(x => x.Flat)
            .WithMany(x => x.FlatMeters)
            .HasForeignKey(x => x.FlatId);
        builder
            .HasOne(x => x.Meter)
            .WithMany(x => x.FlatMeters)
            .HasForeignKey(x => x.MetterId);
    }
}