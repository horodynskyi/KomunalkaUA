﻿// <auto-generated />
using System;
using KomunalkaUA.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Building")
                        .HasColumnType("text");

                    b.Property<string>("FlatNumber")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.CallbackMessage", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("CallbackMessages");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Checkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("FlatId")
                        .HasColumnType("integer");

                    b.Property<int?>("TariffId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FlatId");

                    b.HasIndex("TariffId");

                    b.ToTable("Checkout");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Flat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AddressId")
                        .HasColumnType("integer");

                    b.Property<string>("CardNumber")
                        .HasColumnType("text");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TenantId");

                    b.ToTable("Flats");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.FlatMeter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("FlatId")
                        .HasColumnType("integer");

                    b.Property<int?>("MetterId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FlatId");

                    b.HasIndex("MetterId");

                    b.ToTable("FlatMeter");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Meter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MeterType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.Property<int?>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Meters");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleType")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleType = "Owner"
                        },
                        new
                        {
                            Id = 2,
                            RoleType = "Tenant"
                        });
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("StateType")
                        .HasColumnType("text");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Tariff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double?>("Electric")
                        .HasColumnType("double precision");

                    b.Property<double?>("Gas")
                        .HasColumnType("double precision");

                    b.Property<int>("RentRate")
                        .HasColumnType("integer");

                    b.Property<double?>("Watter")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Tariffs");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("SecondName")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Checkout", b =>
                {
                    b.HasOne("KomunalkaUA.Domain.Models.Flat", "Flat")
                        .WithMany("Checkouts")
                        .HasForeignKey("FlatId");

                    b.HasOne("KomunalkaUA.Domain.Models.Tariff", "Tariff")
                        .WithMany("Checkouts")
                        .HasForeignKey("TariffId");

                    b.Navigation("Flat");

                    b.Navigation("Tariff");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Flat", b =>
                {
                    b.HasOne("KomunalkaUA.Domain.Models.Address", "Address")
                        .WithMany("Flats")
                        .HasForeignKey("AddressId");

                    b.HasOne("KomunalkaUA.Domain.Models.User", "Owner")
                        .WithMany("Owners")
                        .HasForeignKey("OwnerId");

                    b.HasOne("KomunalkaUA.Domain.Models.User", "Tenant")
                        .WithMany("Tenants")
                        .HasForeignKey("TenantId");

                    b.Navigation("Address");

                    b.Navigation("Owner");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.FlatMeter", b =>
                {
                    b.HasOne("KomunalkaUA.Domain.Models.Flat", "Flat")
                        .WithMany("FlatMeters")
                        .HasForeignKey("FlatId");

                    b.HasOne("KomunalkaUA.Domain.Models.Meter", "Meter")
                        .WithMany("FlatMeters")
                        .HasForeignKey("MetterId");

                    b.Navigation("Flat");

                    b.Navigation("Meter");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.State", b =>
                {
                    b.HasOne("KomunalkaUA.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.User", b =>
                {
                    b.HasOne("KomunalkaUA.Domain.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Address", b =>
                {
                    b.Navigation("Flats");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Flat", b =>
                {
                    b.Navigation("Checkouts");

                    b.Navigation("FlatMeters");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Meter", b =>
                {
                    b.Navigation("FlatMeters");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Tariff", b =>
                {
                    b.Navigation("Checkouts");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.User", b =>
                {
                    b.Navigation("Owners");

                    b.Navigation("Tenants");
                });
#pragma warning restore 612, 618
        }
    }
}
