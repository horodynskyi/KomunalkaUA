﻿// <auto-generated />
using System;
using KomunalkaUA.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220627090918_ChangedCheckoutFields")]
    partial class ChangedCheckoutFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("CityId")
                        .HasColumnType("integer");

                    b.Property<string>("FlatNumber")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

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

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("FlatId")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentSum")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FlatId");

                    b.ToTable("Checkout");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Region")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Чернівці",
                            Region = "Чернівецький"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Тернопіль",
                            Region = "Тернопільський"
                        });
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

                    b.Property<int?>("Rent")
                        .HasColumnType("integer");

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

            modelBuilder.Entity("KomunalkaUA.Domain.Models.FlatPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int?>("FlatId")
                        .HasColumnType("integer");

                    b.Property<string>("PhotoId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FlatId");

                    b.HasIndex("PhotoId");

                    b.ToTable("FlatPhotos");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Meter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.Property<int?>("ProviderId")
                        .HasColumnType("integer");

                    b.Property<int?>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("Meters");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.MeterType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MeterTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Газ"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Вода"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Світло"
                        });
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Photo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.PreMeterCheckout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("EndValue")
                        .HasColumnType("integer");

                    b.Property<int?>("FlatId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<int?>("MeterId")
                        .HasColumnType("integer");

                    b.Property<int?>("StartValue")
                        .HasColumnType("integer");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FlatId");

                    b.HasIndex("MeterId");

                    b.HasIndex("TenantId");

                    b.ToTable("PreMeterCheckouts");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CityId")
                        .HasColumnType("integer");

                    b.Property<int?>("MeterTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double?>("Rate")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("MeterTypeId");

                    b.ToTable("Providers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            MeterTypeId = 1,
                            Name = "Нафтогаз",
                            Rate = 7.9900000000000002
                        });
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
                        .IsRequired()
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

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Address", b =>
                {
                    b.HasOne("KomunalkaUA.Domain.Models.City", "City")
                        .WithMany("Addresses")
                        .HasForeignKey("CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Checkout", b =>
                {
                    b.HasOne("KomunalkaUA.Domain.Models.Flat", "Flat")
                        .WithMany("Checkouts")
                        .HasForeignKey("FlatId");

                    b.HasOne("KomunalkaUA.Domain.Models.FlatMeter", null)
                        .WithMany("Checkouts")
                        .HasForeignKey("FlatMeterId");

                    b.HasOne("KomunalkaUA.Domain.Models.Tariff", null)
                        .WithMany("Checkouts")
                        .HasForeignKey("TariffId");

                    b.Navigation("Flat");
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

            modelBuilder.Entity("KomunalkaUA.Domain.Models.FlatPhoto", b =>
                {
                    b.HasOne("KomunalkaUA.Domain.Models.Flat", "Flat")
                        .WithMany("Photos")
                        .HasForeignKey("FlatId");

                    b.HasOne("KomunalkaUA.Domain.Models.Photo", "Photo")
                        .WithMany("FlatPhotos")
                        .HasForeignKey("PhotoId");

                    b.Navigation("Flat");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Meter", b =>
                {
                    b.HasOne("KomunalkaUA.Domain.Models.Provider", "Provider")
                        .WithMany("Meters")
                        .HasForeignKey("ProviderId");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.PreMeterCheckout", b =>
                {
                    b.HasOne("KomunalkaUA.Domain.Models.Flat", "Flat")
                        .WithMany("PreMeterCheckouts")
                        .HasForeignKey("FlatId");

                    b.HasOne("KomunalkaUA.Domain.Models.Meter", "Meter")
                        .WithMany("PreMeterCheckouts")
                        .HasForeignKey("MeterId");

                    b.HasOne("KomunalkaUA.Domain.Models.User", "Tenant")
                        .WithMany("PreMeterCheckouts")
                        .HasForeignKey("TenantId");

                    b.Navigation("Flat");

                    b.Navigation("Meter");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Provider", b =>
                {
                    b.HasOne("KomunalkaUA.Domain.Models.City", "City")
                        .WithMany("Providers")
                        .HasForeignKey("CityId");

                    b.HasOne("KomunalkaUA.Domain.Models.MeterType", "Type")
                        .WithMany("Providers")
                        .HasForeignKey("MeterTypeId");

                    b.Navigation("City");

                    b.Navigation("Type");
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

            modelBuilder.Entity("KomunalkaUA.Domain.Models.City", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Providers");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Flat", b =>
                {
                    b.Navigation("Checkouts");

                    b.Navigation("FlatMeters");

                    b.Navigation("Photos");

                    b.Navigation("PreMeterCheckouts");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.FlatMeter", b =>
                {
                    b.Navigation("Checkouts");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Meter", b =>
                {
                    b.Navigation("FlatMeters");

                    b.Navigation("PreMeterCheckouts");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.MeterType", b =>
                {
                    b.Navigation("Providers");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Photo", b =>
                {
                    b.Navigation("FlatPhotos");
                });

            modelBuilder.Entity("KomunalkaUA.Domain.Models.Provider", b =>
                {
                    b.Navigation("Meters");
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

                    b.Navigation("PreMeterCheckouts");

                    b.Navigation("Tenants");
                });
#pragma warning restore 612, 618
        }
    }
}