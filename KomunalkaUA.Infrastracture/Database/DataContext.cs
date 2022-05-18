using KomunalkaUA.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace KomunalkaUA.Infrastracture.Database;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Flat> Flats { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<ElectricMeter> ElectricMeters { get; set; }
    public DbSet<GasMeter> GasMeters { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Tariff> Tariffs { get; set; }
    public DbSet<WatterMeter> WatterMeters { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}