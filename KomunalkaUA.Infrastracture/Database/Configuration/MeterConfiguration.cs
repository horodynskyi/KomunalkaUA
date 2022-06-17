using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class MeterConfiguration:IEntityTypeConfiguration<Meter>
{
    public void Configure(EntityTypeBuilder<Meter> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Number);
        builder
            .Property(x => x.Value)
            .HasColumnType("integer");

        builder
            .HasOne(x => x.Provider)
            .WithMany(x => x.Meters)
            .HasForeignKey(x => x.ProviderId);
        /*builder
            .Property(x => x.MeterType)
            .HasConversion(
                v => v
                v => (MeterType) v);
            );*/
    }
}