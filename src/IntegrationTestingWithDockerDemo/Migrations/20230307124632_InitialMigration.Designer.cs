﻿// <auto-generated />
using System;
using IntegrationTestingWithDockerDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IntegrationTestingWithDockerDemo.Migrations
{
    [DbContext(typeof(MySqlDbContext))]
    [Migration("20230307124632_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IntegrationTestingWithDockerDemo.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<bool?>("Active")
                        .HasColumnType("tinyint(1)")
                        .HasColumnOrder(6);

                    b.Property<string>("Address")
                        .HasColumnType("longtext")
                        .HasColumnOrder(3);

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime(6)")
                        .HasColumnOrder(5);

                    b.Property<string>("Email")
                        .HasColumnType("longtext")
                        .HasColumnOrder(4);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.ToTable("Students");
                });
#pragma warning restore 612, 618
        }
    }
}