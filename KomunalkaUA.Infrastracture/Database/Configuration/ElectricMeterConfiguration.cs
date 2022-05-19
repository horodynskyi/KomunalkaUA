using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class ElectricMeterConfiguration:IEntityTypeConfiguration<ElectricMeter>
{
    public void Configure(EntityTypeBuilder<ElectricMeter> builder)
    {
        builder
            .Property(x => x.Id);
        
    }
}