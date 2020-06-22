﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.Infra.Context;

namespace Services.Infra.Migrations
{
    [DbContext(typeof(ContextBase))]
    [Migration("20200505031628_userPoints")]
    partial class userPoints
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Services.Domain.Entities.AirPlane", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberOfPassengers")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("AirPlane");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                            Code = "1",
                            CreationDate = new DateTime(2020, 5, 5, 0, 16, 27, 495, DateTimeKind.Local).AddTicks(5393),
                            ModelId = new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc1"),
                            NumberOfPassengers = 111
                        },
                        new
                        {
                            Id = new Guid("a714554f-f363-42f1-b41a-81ee85186622"),
                            Code = "3B",
                            CreationDate = new DateTime(2020, 5, 5, 0, 16, 27, 502, DateTimeKind.Local).AddTicks(2100),
                            ModelId = new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc3"),
                            NumberOfPassengers = 167
                        },
                        new
                        {
                            Id = new Guid("a714554f-f363-42f1-b41a-81ee85186661"),
                            Code = "4",
                            CreationDate = new DateTime(2020, 5, 5, 0, 16, 27, 502, DateTimeKind.Local).AddTicks(2218),
                            ModelId = new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc4"),
                            NumberOfPassengers = 117
                        });
                });

            modelBuilder.Entity("Services.Domain.Entities.AirPlaneModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("AirPlaneModel");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc1"),
                            Name = "Airbus A300B1"
                        },
                        new
                        {
                            Id = new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc2"),
                            Name = "Airbus A319"
                        },
                        new
                        {
                            Id = new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc3"),
                            Name = "Boeing 737-100"
                        },
                        new
                        {
                            Id = new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc4"),
                            Name = "Boeing CRJ-100"
                        });
                });

            modelBuilder.Entity("Services.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BarCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DueDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaidDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Payment");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                            BarCode = "0000 1111 2222 3333 4444",
                            Cost = "1.395,20",
                            Description = "Rent",
                            DueDate = "05/04/2020",
                            Link = "https://www.portalunsoft.com.br/area-do-cliente/safira/",
                            Notes = "Notes",
                            PaidDate = "05/04/2020",
                            PaymentType = "Juridico"
                        });
                });

            modelBuilder.Entity("Services.Domain.Entities.Questions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CorrectOption")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                            CorrectOption = "Option2",
                            Option1 = "1",
                            Option2 = "2",
                            Option3 = "3",
                            Option4 = "4",
                            Question = "Quanto é 1 + 1"
                        });
                });

            modelBuilder.Entity("Services.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b413cfc0-f53a-4765-9430-3912efcd79c5"),
                            Password = "Password",
                            UserName = "UserName"
                        });
                });

            modelBuilder.Entity("Services.Domain.Entities.UserPoints", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserPoints");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b413cfc0-f53a-4765-9430-3912efcd79c5"),
                            Points = 0,
                            UserName = "UserName"
                        });
                });

            modelBuilder.Entity("Services.Domain.Entities.AirPlane", b =>
                {
                    b.HasOne("Services.Domain.Entities.AirPlaneModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
