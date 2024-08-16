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

                    b.Property<bool>("IsAvtive")
                        .HasColumnType("bit");

                    b.HasKey("FolderMediaID");

                    b.ToTable("FolderMediaModel");
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
#pragma warning restore 612, 618
        }
    }
}