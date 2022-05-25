﻿using KomunalkaUA.Domain.Models;
using KomunalkaUA.Infrastracture.Database.Configuration;
using Microsoft.EntityFrameworkCore;


namespace KomunalkaUA.Infrastracture.Database;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DataContext()
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Flat> Flats { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Tariff> Tariffs { get; set; }
    public DbSet<Meter> Meters { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<CallbackMessage> CallbackMessages { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<FlatPhoto> FlatPhotos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new CheckoutConfiguration());
        modelBuilder.ApplyConfiguration(new MeterConfiguration());
        modelBuilder.ApplyConfiguration(new FlatConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new TariffConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new StateConfiguration());
        modelBuilder.ApplyConfiguration(new FlatMeterConfiguration());
        modelBuilder.ApplyConfiguration(new CallbackMessageConfiguration());
        modelBuilder.ApplyConfiguration(new PhotoConfiguration());
        modelBuilder.ApplyConfiguration(new FlatPhotoConfiguration());
    }
}