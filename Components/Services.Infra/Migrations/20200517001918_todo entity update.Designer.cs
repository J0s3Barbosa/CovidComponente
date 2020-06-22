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
    [Migration("20200517001918_todo entity update")]
    partial class todoentityupdate
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
                            CreationDate = new DateTime(2020, 5, 16, 21, 19, 17, 119, DateTimeKind.Local).AddTicks(8538),
                            ModelId = new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc1"),
                            NumberOfPassengers = 111
                        },
                        new
                        {
                            Id = new Guid("a714554f-f363-42f1-b41a-81ee85186622"),
                            Code = "3B",
                            CreationDate = new DateTime(2020, 5, 16, 21, 19, 17, 120, DateTimeKind.Local).AddTicks(7036),
                            ModelId = new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc3"),
                            NumberOfPassengers = 167
                        },
                        new
                        {
                            Id = new Guid("a714554f-f363-42f1-b41a-81ee85186661"),
                            Code = "4",
                            CreationDate = new DateTime(2020, 5, 16, 21, 19, 17, 120, DateTimeKind.Local).AddTicks(7108),
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

            modelBuilder.Entity("Services.Domain.Entities.Todo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TaskComplete")
                        .HasColumnType("bit");

                    b.Property<string>("TimeFinished")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeStarted")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalTimeLapsed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Todo");

                    b.HasData(
                        new
                        {
                            Id = new Guid("644276de-5007-41e6-9457-343fa1521855"),
                            Description = "Description",
                            Details = "Details",
                            TaskComplete = false,
                            TimeFinished = "5/16/2020 11:19:17 PM",
                            TimeStarted = "5/16/2020 9:19:17 PM",
                            UserEmail = "Email@Email.com"
                        });
                });

            modelBuilder.Entity("Services.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b413cfc0-f53a-4765-9430-3912efcd79c5"),
                            Email = "Email@Email.com",
                            FirstName = "FirstName",
                            LastName = "LastName",
                            Password = "Password",
                            Phone = "99999999999"
                        });
                });

            modelBuilder.Entity("Services.Domain.Entities.UserPoints", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserPoints");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b413cfc0-f53a-4765-9430-3912efcd79c5"),
                            Email = "Email@Email.com",
                            Points = 0
                        });
                });

            modelBuilder.Entity("Services.Domain.Entities.VideoPlayer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VideoPlayer");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e404fe71-ddee-4173-9b8d-83bd444fafa1"),
                            Description = "Description",
                            Link = "https://www.youtube.com/watch?v=4MkuId9X-hk",
                            UserEmail = "Email@Email.com"
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