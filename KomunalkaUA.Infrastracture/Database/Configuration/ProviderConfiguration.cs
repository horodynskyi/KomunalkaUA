using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class ProviderConfiguration:IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.City)
            .WithMany(x => x.Providers)
            .HasForeignKey(x => x.CityId);
        builder
            .HasOne(x => x.Type)
            .WithMany(x => x.Providers)
            .HasForeignKey(x => x.MeterTypeId);
        builder
            .HasData(new List<Provider>()
            {
                new()
                {
                    Id = 1,
                    CityId = 1,
                    Name = "Нафтогаз",
                    Rate = 7.99,
                    MeterTypeId = (int) MeterTypeEnum.Gas
                }
            });
    }
}