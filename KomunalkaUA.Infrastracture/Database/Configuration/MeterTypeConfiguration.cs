using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class MeterTypeConfiguration:IEntityTypeConfiguration<MeterType>
{
    public void Configure(EntityTypeBuilder<MeterType> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasData(new List<MeterType>()
            {
                new()
                {
                    Id = 1,
                    Name = "Газ",
                }, 
                new()
                {
                    Id = 2,
                    Name = "Вода"
                },
                new()
                {
                    Id = 3,
                    Name = "Світло"
                },
            });
    }
}