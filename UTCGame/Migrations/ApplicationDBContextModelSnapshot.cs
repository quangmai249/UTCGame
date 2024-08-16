﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UTCGame.Data;

#nullable disable

namespace UTCGame.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UTCGame.Areas.Employee.Models.EmployeeModel", b =>
                {
                    b.Property<Guid>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmployeeEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeePassword")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<bool>("IsEmployeeActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("RegionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmployeeID");

                    b.HasIndex("RegionID");

                    b.HasIndex("RoleID");

                    b.ToTable("EmployeeModel");
                });

            modelBuilder.Entity("UTCGame.Areas.Employee.Models.Region", b =>
                {
                    b.Property<Guid>("RegionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRegionActive")
                        .HasColumnType("bit");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RegionID");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("UTCGame.Areas.Employee.Models.Role", b =>
                {
                    b.Property<Guid>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRoleActive")
                        .HasColumnType("bit");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("UTCGame.Areas.FolderMedia.Models.FolderMediaModel", b =>
                {
                    b.Property<Guid>("FolderMediaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FolderMediaName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("FolderMediaID");

                    b.ToTable("FolderMediaModel");
                });

            modelBuilder.Entity("UTCGame.Areas.Game.Models.GameModel", b =>
                {
                    b.Property<Guid>("GameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FolderMediaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GamePlatform")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("GamePrice")
                        .HasColumnType("real");

                    b.Property<DateTime>("GameReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GameType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsGameActive")
                        .HasColumnType("bit");

                    b.HasKey("GameID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("FolderMediaID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("UTCGame.Areas.Game.Models.GamePlatform", b =>
                {
                    b.Property<Guid>("GamePlatformID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GamePlatformLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GamePlatformName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("GamePlatformID");

                    b.ToTable("GamePlatform");
                });

            modelBuilder.Entity("UTCGame.Areas.Game.Models.GameType", b =>
                {
                    b.Property<Guid>("GameTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GameTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("GameTypeID");

                    b.ToTable("GameType");
                });

            modelBuilder.Entity("UTCGame.Areas.News.Models.NewEvent", b =>
                {
                    b.Property<Guid>("NewEventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FolderMediaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("NewEventDateTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewEventDetail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewEventTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NewsCategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("NewEventID");

                    b.HasIndex("FolderMediaID");

                    b.HasIndex("NewsCategoryID");

                    b.ToTable("NewEvent");
                });

            modelBuilder.Entity("UTCGame.Areas.News.Models.NewsCategory", b =>
                {
                    b.Property<Guid>("NewsCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("NewsCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NewsCategoryID");

                    b.ToTable("NewsCategory");
                });

            modelBuilder.Entity("UTCGame.Areas.Product.Models.ProductModel", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsProductActive")
                        .HasColumnType("bit");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ProductPrice")
                        .HasColumnType("real");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("ProductReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductID");

                    b.HasIndex("GameID");

                    b.HasIndex("ProductTypeID");

                    b.ToTable("ProductModel");
                });

            modelBuilder.Entity("UTCGame.Areas.Product.Models.ProductType", b =>
                {
                    b.Property<Guid>("ProductTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ProductTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductTypeID");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("UTCGame.Areas.Recruit.Models.RecruitModel", b =>
                {
                    b.Property<Guid>("RecruitID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("RecruitName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RecruitID");

                    b.HasIndex("RegionID");

                    b.ToTable("RecruitModel");
                });

            modelBuilder.Entity("UTCGame.Areas.Employee.Models.EmployeeModel", b =>
                {
                    b.HasOne("UTCGame.Areas.Employee.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UTCGame.Areas.Employee.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("UTCGame.Areas.Game.Models.GameModel", b =>
                {
                    b.HasOne("UTCGame.Areas.Employee.Models.EmployeeModel", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UTCGame.Areas.FolderMedia.Models.FolderMediaModel", "FolderMedia")
                        .WithMany()
                        .HasForeignKey("FolderMediaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("FolderMedia");
                });

            modelBuilder.Entity("UTCGame.Areas.News.Models.NewEvent", b =>
                {
                    b.HasOne("UTCGame.Areas.FolderMedia.Models.FolderMediaModel", "FolderMediaModel")
                        .WithMany()
                        .HasForeignKey("FolderMediaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UTCGame.Areas.News.Models.NewsCategory", "NewsCategory")
                        .WithMany()
                        .HasForeignKey("NewsCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FolderMediaModel");

                    b.Navigation("NewsCategory");
                });

            modelBuilder.Entity("UTCGame.Areas.Product.Models.ProductModel", b =>
                {
                    b.HasOne("UTCGame.Areas.Game.Models.GameModel", "Game")
                        .WithMany()
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UTCGame.Areas.Product.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("UTCGame.Areas.Recruit.Models.RecruitModel", b =>
                {
                    b.HasOne("UTCGame.Areas.Employee.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
