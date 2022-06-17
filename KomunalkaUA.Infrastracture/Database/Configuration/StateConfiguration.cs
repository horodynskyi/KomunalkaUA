using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class StateConfiguration:IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Value)
            .HasColumnType("jsonb");
        builder
            .Property(x => x.StateType)
            .HasConversion(
                v => v.ToString(),
                v => (StateType) Enum.Parse(typeof(StateType), v)
            );
    }
}