﻿// <auto-generated />
using System;
using CarGarageParking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarGarageParking.Migrations
{
    [DbContext(typeof(CarGarageParkingDbContext))]
    [Migration("20250404153212_nullGarageInVehicleInGarage")]
    partial class nullGarageInVehicleInGarage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarGarageParking.Models.Application", b =>
                {
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicationId"));

                    b.Property<decimal>("Credit")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("HasActiveMembership")
                        .HasColumnType("bit");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("ApplicationId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("CarGarageParking.Models.Garage", b =>
                {
                    b.Property<int>("GarageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GarageId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("CurrentOccupancy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastEntryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("GarageId");

                    b.ToTable("Garages");
                });

            modelBuilder.Entity("CarGarageParking.Models.Owner", b =>
                {
                    b.Property<int>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OwnerId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("OwnerId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("CarGarageParking.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PaymentTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalCharge")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("VehicleHourlyRate")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VehicleInGarageId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("VehicleInGarageId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("CarGarageParking.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("CarGarageParking.Models.VehicleInGarage", b =>
                {
                    b.Property<int>("VehicleInGarageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleInGarageId"));

                    b.Property<DateTime>("EntryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExitTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GarageId")
                        .HasColumnType("int");

                    b.Property<decimal>("HourlyRate")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsVehicleStillInGarage")
                        .HasColumnType("bit");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("VehicleInGarageId");

                    b.HasIndex("GarageId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleInGarages");
                });

            modelBuilder.Entity("CarGarageParking.Models.Application", b =>
                {
                    b.HasOne("CarGarageParking.Models.Owner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("CarGarageParking.Models.Payment", b =>
                {
                    b.HasOne("CarGarageParking.Models.VehicleInGarage", "VehicleInGarage")
                        .WithMany()
                        .HasForeignKey("VehicleInGarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleInGarage");
                });

            modelBuilder.Entity("CarGarageParking.Models.Vehicle", b =>
                {
                    b.HasOne("CarGarageParking.Models.Application", "Application")
                        .WithMany("Vehicles")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("CarGarageParking.Models.Owner", "Owner")
                        .WithMany("Vehicles")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Application");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("CarGarageParking.Models.VehicleInGarage", b =>
                {
                    b.HasOne("CarGarageParking.Models.Garage", "Garage")
                        .WithMany("VehicleInGarages")
                        .HasForeignKey("GarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarGarageParking.Models.Owner", "Owner")
                        .WithMany("VehicleInGarages")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("CarGarageParking.Models.Vehicle", "Vehicle")
                        .WithMany("VehicleInGarages")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garage");

                    b.Navigation("Owner");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("CarGarageParking.Models.Application", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("CarGarageParking.Models.Garage", b =>
                {
                    b.Navigation("VehicleInGarages");
                });

            modelBuilder.Entity("CarGarageParking.Models.Owner", b =>
                {
                    b.Navigation("VehicleInGarages");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("CarGarageParking.Models.Vehicle", b =>
                {
                    b.Navigation("VehicleInGarages");
                });
#pragma warning restore 612, 618
        }
    }
}
