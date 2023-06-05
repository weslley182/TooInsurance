﻿// <auto-generated />
using System;
using DataBaseModel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataBaseModel.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("DataBaseModel.Model.CarParcelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MessageId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Parcel")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CarParcels");
                });

            modelBuilder.Entity("DataBaseModel.Model.HomeParcelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MessageId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Parcel")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("HomeParcels");
                });

            modelBuilder.Entity("DataBaseModel.Model.MessageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
