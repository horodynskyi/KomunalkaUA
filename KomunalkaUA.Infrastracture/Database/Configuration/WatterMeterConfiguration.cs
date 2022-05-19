using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class WatterMeterConfiguration:IEntityTypeConfiguration<WatterMeter>
{
    public void Configure(EntityTypeBuilder<WatterMeter> builder)
    {
        builder
            .Property(x => x.Id);
        
    }
}