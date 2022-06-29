using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class CityConfiguration:IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasData(new List<City>()
            {
                new()
                {
                    Id = 1,
                    Name = "Чернівці",
                    Region = "Чернівецький"
                }, 
                new()
                {
                    Id = 2,
                    Name = "Тернопіль",
                    Region = "Чернівецький"
                }
            });
    }
}