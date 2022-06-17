﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftUniFest.Data;

#nullable disable

namespace SoftUniFest.Data.Migrations.ApplicationDbContext2Migrations
{
    [DbContext(typeof(ApplicationDbContext2))]
    [Migration("20220617112441_RemoveIdentityTables")]
    partial class RemoveIdentityTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SoftUniFest.Data.Models.App.AppEmployee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SoftUniFest.Data.Models.App.AppPosTerminal", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TraderId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TraderId");

                    b.ToTable("PosTerminals");
                });

            modelBuilder.Entity("SoftUniFest.Data.Models.App.AppTrader", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateOfRegister")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Traders");
                });

            modelBuilder.Entity("SoftUniFest.Data.Models.App.CardHolder", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfRegister")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CardHolders");
                });

            modelBuilder.Entity("SoftUniFest.Data.Models.App.Discount", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TraderId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TraderId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("SoftUniFest.Data.Models.App.AppPosTerminal", b =>
                {
                    b.HasOne("SoftUniFest.Data.Models.App.AppTrader", "Trader")
                        .WithMany("PosTerminals")
                        .HasForeignKey("TraderId");

                    b.Navigation("Trader");
                });

            modelBuilder.Entity("SoftUniFest.Data.Models.App.Discount", b =>
                {
                    b.HasOne("SoftUniFest.Data.Models.App.AppTrader", "Trader")
                        .WithMany("Discounts")
                        .HasForeignKey("TraderId");

                    b.Navigation("Trader");
                });

            modelBuilder.Entity("SoftUniFest.Data.Models.App.AppTrader", b =>
                {
                    b.Navigation("Discounts");

                    b.Navigation("PosTerminals");
                });
#pragma warning restore 612, 618
        }
    }
}
