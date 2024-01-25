﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Product.API.InventoryManagement.Infrastructure.Configuration;

#nullable disable

namespace Product.API.InventoryManagement.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    partial class InventoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Product.API.InventoryManagement.Infrastructure.Entities.InventoryDetailsEntity", b =>
                {
                    b.Property<int>("FactorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FactorId"));

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBuy")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSell")
                        .HasColumnType("bit");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("FactorId");

                    b.HasIndex("ProductId");

                    b.ToTable("InventoryDetails");
                });

            modelBuilder.Entity("Product.API.InventoryManagement.Infrastructure.Entities.InventoryEntity", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastDateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("InventoryProducts");
                });

            modelBuilder.Entity("Product.API.InventoryManagement.Infrastructure.Entities.InventoryDetailsEntity", b =>
                {
                    b.HasOne("Product.API.InventoryManagement.Infrastructure.Entities.InventoryEntity", "Inventory")
                        .WithMany("InventoryDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("Product.API.InventoryManagement.Infrastructure.Entities.InventoryEntity", b =>
                {
                    b.Navigation("InventoryDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
