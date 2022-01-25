﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrashTalker.Database;

namespace TrashTalker.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220112223304_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrashTalker.Models.Alert", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("alertStatus")
                        .HasColumnType("int");

                    b.Property<int>("alertType")
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("employeeid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("issue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("employeeid");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("TrashTalker.Models.CollectPoint", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("order")
                        .HasColumnType("int");

                    b.Property<Guid?>("recycleBinid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("routeid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("recycleBinid");

                    b.HasIndex("routeid");

                    b.ToTable("CollectPoint");
                });

            modelBuilder.Entity("TrashTalker.Models.Container", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RecBinid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("avgGrowthOccupiedVolumePerDay")
                        .HasColumnType("float");

                    b.Property<float>("currentPercOccupied")
                        .HasColumnType("real");

                    b.Property<DateTime?>("dateFull")
                        .HasColumnType("datetime2");

                    b.Property<float>("depth")
                        .HasColumnType("real");

                    b.Property<float>("height")
                        .HasColumnType("real");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int>("typeOfWaste")
                        .HasColumnType("int");

                    b.Property<float>("width")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.HasIndex("RecBinid");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("TrashTalker.Models.Measurement", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("containerid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<int>("distance")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("containerid");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("TrashTalker.Models.Picking", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("avgGrowthOccupiedPerDay")
                        .HasColumnType("float");

                    b.Property<Guid?>("containerid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("data")
                        .HasColumnType("datetime2");

                    b.Property<float>("volumeRecolhido")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.HasIndex("containerid");

                    b.ToTable("Pickings");
                });

            modelBuilder.Entity("TrashTalker.Models.RecycleBin", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("latit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("longit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("zipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("RecycleBins");
                });

            modelBuilder.Entity("TrashTalker.Models.Route", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("dateBegin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dateCriation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateEnd")
                        .HasColumnType("datetime2");

                    b.Property<int>("distanceEstimatedKm")
                        .HasColumnType("int");

                    b.Property<float?>("distanceTravelledKm")
                        .HasColumnType("real");

                    b.Property<Guid>("employeeid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("estimatedDuration")
                        .HasColumnType("time");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int>("typeCreation")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("employeeid");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("TrashTalker.Models.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("role")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("zipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TrashTalker.Models.Alert", b =>
                {
                    b.HasOne("TrashTalker.Models.User", "employee")
                        .WithMany()
                        .HasForeignKey("employeeid");

                    b.Navigation("employee");
                });

            modelBuilder.Entity("TrashTalker.Models.CollectPoint", b =>
                {
                    b.HasOne("TrashTalker.Models.RecycleBin", "recycleBin")
                        .WithMany("routes")
                        .HasForeignKey("recycleBinid");

                    b.HasOne("TrashTalker.Models.Route", "route")
                        .WithMany("collectPoints")
                        .HasForeignKey("routeid");

                    b.Navigation("recycleBin");

                    b.Navigation("route");
                });

            modelBuilder.Entity("TrashTalker.Models.Container", b =>
                {
                    b.HasOne("TrashTalker.Models.RecycleBin", "RecBin")
                        .WithMany("containers")
                        .HasForeignKey("RecBinid");

                    b.Navigation("RecBin");
                });

            modelBuilder.Entity("TrashTalker.Models.Measurement", b =>
                {
                    b.HasOne("TrashTalker.Models.Container", "container")
                        .WithMany()
                        .HasForeignKey("containerid");

                    b.Navigation("container");
                });

            modelBuilder.Entity("TrashTalker.Models.Picking", b =>
                {
                    b.HasOne("TrashTalker.Models.Container", "container")
                        .WithMany("listPickings")
                        .HasForeignKey("containerid");

                    b.Navigation("container");
                });

            modelBuilder.Entity("TrashTalker.Models.Route", b =>
                {
                    b.HasOne("TrashTalker.Models.User", "employee")
                        .WithMany("routes")
                        .HasForeignKey("employeeid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("employee");
                });

            modelBuilder.Entity("TrashTalker.Models.Container", b =>
                {
                    b.Navigation("listPickings");
                });

            modelBuilder.Entity("TrashTalker.Models.RecycleBin", b =>
                {
                    b.Navigation("containers");

                    b.Navigation("routes");
                });

            modelBuilder.Entity("TrashTalker.Models.Route", b =>
                {
                    b.Navigation("collectPoints");
                });

            modelBuilder.Entity("TrashTalker.Models.User", b =>
                {
                    b.Navigation("routes");
                });
#pragma warning restore 612, 618
        }
    }
}
