﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Portfolio.Data;

#nullable disable

namespace Portfolio.Migrations
{
    [DbContext(typeof(AppliDB))]
    [Migration("20231021224701_x100")]
    partial class x100
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Portfolio.Models.Investment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bank_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Investment_Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("Investment_Start_Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Maturity_Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("Maturity_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ROI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time_Left_To_Mature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uemail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("lastUpdate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("investments");
                });

            modelBuilder.Entity("Portfolio.Models.Logs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bank_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Investment_Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("Investment_Start_Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Maturity_Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("Maturity_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ROI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uemail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("logs");
                });

            modelBuilder.Entity("Portfolio.Models.User", b =>
                {
                    b.Property<string>("EmailId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.HasKey("EmailId");

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}
