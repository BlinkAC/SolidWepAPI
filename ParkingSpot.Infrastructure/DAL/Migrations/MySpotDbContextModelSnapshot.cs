﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ParkingSpot.Infrastructure.DAL.Repositories;

#nullable disable

namespace ParkingSpot.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(MySpotDbContext))]
    partial class MySpotDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ParkingSpot.Core.Entities.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("WeeklyParkingSpotId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WeeklyParkingSpotId");

                    b.ToTable("reservations");

                    b.HasDiscriminator<string>("Type").HasValue("Reservation");
                });

            modelBuilder.Entity("ParkingSpot.Core.Entities.WeeklyParkingSpot", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Week")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("weeklyParkingSpots");
                });

            modelBuilder.Entity("ParkingSpot.Core.Entities.CleaningReservation", b =>
                {
                    b.HasBaseType("ParkingSpot.Core.Entities.Reservation");

                    b.HasDiscriminator().HasValue("CleaningReservation");
                });

            modelBuilder.Entity("ParkingSpot.Core.Entities.VehicleReservation", b =>
                {
                    b.HasBaseType("ParkingSpot.Core.Entities.Reservation");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("text");

                    b.Property<string>("LicensePlate")
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("VehicleReservation");
                });

            modelBuilder.Entity("ParkingSpot.Core.Entities.Reservation", b =>
                {
                    b.HasOne("ParkingSpot.Core.Entities.WeeklyParkingSpot", null)
                        .WithMany("Reservations")
                        .HasForeignKey("WeeklyParkingSpotId");
                });

            modelBuilder.Entity("ParkingSpot.Core.Entities.WeeklyParkingSpot", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
