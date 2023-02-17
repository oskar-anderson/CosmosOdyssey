﻿// <auto-generated />
using System;
using DAL.App.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.App.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230216142431_DbCreation3")]
    partial class DbCreation3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.App.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ChangedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Domain.App.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ChangedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domain.App.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ChangedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PlanetLocationName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PlanetName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PlanetarySystemName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("UniquePlanetLocation3LetterIdentifier")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Domain.App.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ChangedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfPurchase")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FlightEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FlightStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("Price")
                        .HasPrecision(28, 2)
                        .HasColumnType("double precision");

                    b.Property<string>("RouteName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Domain.App.PriceList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ChangedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("ValidUntil")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ValueJson")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PriceList");
                });

            modelBuilder.Entity("Domain.App.ProvidedRoute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ChangedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("DestinationLocationId")
                        .HasColumnType("uuid");

                    b.Property<long>("Distance")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FlightEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FlightStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("FromLocationId")
                        .HasColumnType("uuid");

                    b.Property<double>("Price")
                        .HasPrecision(28, 2)
                        .HasColumnType("double precision");

                    b.Property<Guid>("PriceListId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DestinationLocationId");

                    b.HasIndex("FromLocationId");

                    b.HasIndex("PriceListId");

                    b.ToTable("ProvidedRoute");
                });

            modelBuilder.Entity("Domain.App.Order", b =>
                {
                    b.HasOne("Domain.App.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Domain.App.ProvidedRoute", b =>
                {
                    b.HasOne("Domain.App.Company", "Company")
                        .WithMany("ProvidedRoutes")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.App.Location", "DestinationLocation")
                        .WithMany("DestinationRoutes")
                        .HasForeignKey("DestinationLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.App.Location", "FromLocation")
                        .WithMany("FromRoutes")
                        .HasForeignKey("FromLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.App.PriceList", "PriceList")
                        .WithMany("ProvidedRoutes")
                        .HasForeignKey("PriceListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("DestinationLocation");

                    b.Navigation("FromLocation");

                    b.Navigation("PriceList");
                });

            modelBuilder.Entity("Domain.App.Company", b =>
                {
                    b.Navigation("ProvidedRoutes");
                });

            modelBuilder.Entity("Domain.App.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.App.Location", b =>
                {
                    b.Navigation("DestinationRoutes");

                    b.Navigation("FromRoutes");
                });

            modelBuilder.Entity("Domain.App.PriceList", b =>
                {
                    b.Navigation("ProvidedRoutes");
                });
#pragma warning restore 612, 618
        }
    }
}