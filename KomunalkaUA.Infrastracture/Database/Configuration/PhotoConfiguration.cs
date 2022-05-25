using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class PhotoConfiguration:IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}