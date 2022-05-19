using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KomunalkaUA.Infrastracture.Database.Configuration;

public class GasMeterConfiguration:IEntityTypeConfiguration<GasMeter>
{
    public void Configure(EntityTypeBuilder<GasMeter> builder)
    {
        builder
            .Property(x => x.Id);
        
    }
}