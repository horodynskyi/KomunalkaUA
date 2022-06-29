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
                },
                new()
                {
                    Id = 2,
                    CityId = 1,
                    Name = "ТОВ 'ЧОЕК'",
                    Rate = 1.70,
                    MeterTypeId = (int) MeterTypeEnum.Electric
                },
                new()
                {
                    Id = 3,
                    CityId = 1,
                    Name = "Чернівці Водоканал",
                    Rate = 28,
                    MeterTypeId = (int) MeterTypeEnum.Watter
                },
                new()
                {
                    Id = 4,
                    CityId = 2,
                    Name = "Газпостач Тернопіль",
                    Rate = 7.99,
                    MeterTypeId = (int) MeterTypeEnum.Gas
                },
                new()
                {
                    Id = 7,
                    CityId = 2,
                    Name = "Тернопільоблгаз",
                    Rate = 7.99,
                    MeterTypeId = (int) MeterTypeEnum.Gas
                },
                new()
                {
                    Id = 5,
                    CityId = 2,
                    Name = "Тернопільобленерго",
                    Rate = 1.70,
                    MeterTypeId = (int) MeterTypeEnum.Electric
                },
                new()
                {
                    Id = 6,
                    CityId = 2,
                    Name = "Тернопільводоканал",
                    Rate = 28,
                    MeterTypeId = (int) MeterTypeEnum.Watter
                },
            });
    }
}