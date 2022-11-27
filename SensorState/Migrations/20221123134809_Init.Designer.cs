﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SensorState.Context;

#nullable disable

namespace SensorState.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221123134809_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SensorState.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusBoundId")
                        .HasColumnType("int");

                    b.HasKey("StatusId");

                    b.HasIndex("StatusBoundId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("SensorState.Models.StatusBound", b =>
                {
                    b.Property<int>("StatusBoundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusBoundId"));

                    b.Property<int>("LowerHotBound")
                        .HasColumnType("int");

                    b.Property<int>("UpperColdBound")
                        .HasColumnType("int");

                    b.HasKey("StatusBoundId");

                    b.ToTable("StatusBounds");
                });

            modelBuilder.Entity("SensorState.Models.Temperature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Temperatures");
                });

            modelBuilder.Entity("SensorState.Models.Status", b =>
                {
                    b.HasOne("SensorState.Models.StatusBound", "StatusBound")
                        .WithMany("Temperatures")
                        .HasForeignKey("StatusBoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatusBound");
                });

            modelBuilder.Entity("SensorState.Models.Temperature", b =>
                {
                    b.HasOne("SensorState.Models.Status", "Status")
                        .WithMany("Temperatures")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("SensorState.Models.Status", b =>
                {
                    b.Navigation("Temperatures");
                });

            modelBuilder.Entity("SensorState.Models.StatusBound", b =>
                {
                    b.Navigation("Temperatures");
                });
#pragma warning restore 612, 618
        }
    }
}
