using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class AddressConfiguration:IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder
            .Property(x => x.Id);
    }
}
public class CallbackMessageConfiguration:IEntityTypeConfiguration<CallbackMessage>
{
    public void Configure(EntityTypeBuilder<CallbackMessage> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();
    }
}