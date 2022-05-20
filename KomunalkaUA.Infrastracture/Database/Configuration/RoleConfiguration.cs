using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class RoleConfiguration:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.RoleType)
            .HasConversion(
                v => v.ToString(),
                v => (RoleType?) Enum.Parse(typeof(StateType), v)
            );
        builder.HasData(new List<Role>
        {
            new()
            {
                Id = 1,
                RoleType = RoleType.Owner
            },
            new()
            {
                Id = 2,
                RoleType = RoleType.Tenant
            },
        });
    }
}