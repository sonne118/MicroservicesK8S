﻿// <auto-generated />
using System;
using Booking.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Booking.Persistence.Migrations
{
    [DbContext(typeof(BookingDbContext))]
    [Migration("20190308022413_OrderStatus")]
    partial class OrderStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Booking.Domain.AggregatesModel.BookingAggregate.BookingOrder", b =>
                {
                    b.Property<string>("BookingOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookingState");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CustomerID");

                    b.Property<string>("Destination");

                    b.Property<string>("NotificationID");

                    b.Property<string>("OrderStatus");

                    b.Property<string>("Origin");

                    b.Property<string>("PaymentID");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("BookingOrderId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Booking.Domain.AggregatesModel.BookingAggregate.BookingOrderDetail", b =>
                {
                    b.Property<string>("BookingOrderDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BookingOrderId");

                    b.Property<string>("PackageDescription");

                    b.Property<string>("PackageType");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("BookingOrderDetailId");

                    b.HasIndex("BookingOrderId");

                    b.ToTable("BookingsDetails");
                });

            modelBuilder.Entity("Booking.Domain.AggregatesModel.BookingAggregate.BookingOrderDetail", b =>
                {
                    b.HasOne("Booking.Domain.AggregatesModel.BookingAggregate.BookingOrder")
                        .WithMany("BookingDetails")
                        .HasForeignKey("BookingOrderId");
                });
#pragma warning restore 612, 618
        }
    }
}