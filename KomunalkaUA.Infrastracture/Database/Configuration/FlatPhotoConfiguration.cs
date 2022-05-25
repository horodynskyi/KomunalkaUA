using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class FlatPhotoConfiguration:IEntityTypeConfiguration<FlatPhoto>
{
    public void Configure(EntityTypeBuilder<FlatPhoto> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();
        builder
            .HasOne(x => x.Flat)
            .WithMany(x => x.Photos)
            .HasForeignKey(x => x.FlatId);
        builder
            .HasOne(x => x.Photo)
            .WithMany(x => x.FlatPhotos)
            .HasForeignKey(x => x.PhotoId);
    }
}