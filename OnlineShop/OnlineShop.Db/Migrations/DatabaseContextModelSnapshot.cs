﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.Db;

namespace OnlineShop.Db.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineShop.Db.BasketItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("BasketId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("BasketItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Comparison", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Comparisons");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Contacts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Favourites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.FilePath", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Files");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Path = "/images/River.jpg",
                            ProductId = 1
                        },
                        new
                        {
                            Id = 2,
                            Path = "/images/Eagles.jpg",
                            ProductId = 2
                        },
                        new
                        {
                            Id = 3,
                            Path = "/images/WineBig.jpg",
                            ProductId = 3
                        },
                        new
                        {
                            Id = 4,
                            Path = "/images/AutumnBig.jpg",
                            ProductId = 4
                        },
                        new
                        {
                            Id = 5,
                            Path = "/images/DinoBig.jpg",
                            ProductId = 5
                        },
                        new
                        {
                            Id = 6,
                            Path = "/images/GirlAndWindBig.jpg",
                            ProductId = 6
                        },
                        new
                        {
                            Id = 7,
                            Path = "/images/PennywiseBig.jpg",
                            ProductId = 7
                        },
                        new
                        {
                            Id = 8,
                            Path = "/images/Depression.jpg",
                            ProductId = 8
                        },
                        new
                        {
                            Id = 9,
                            Path = "/images/LaraCroftBig.jpg",
                            ProductId = 9
                        },
                        new
                        {
                            Id = 10,
                            Path = "/images/Morning.jpg",
                            ProductId = 10
                        },
                        new
                        {
                            Id = 11,
                            Path = "/images/Early.jpg",
                            ProductId = 11
                        },
                        new
                        {
                            Id = 12,
                            Path = "/images/Past.jpg",
                            ProductId = 12
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ContactsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateofDelivery")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateofOrder")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAccept")
                        .HasColumnType("bit");

                    b.Property<string>("Mailto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Packaging")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TimeFromTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContactsId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<bool>("IsPromo")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaintingTechnique")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SizeId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cost = 10000m,
                            Description = "Летний пейзаж",
                            Genre = 0,
                            IsPromo = false,
                            Name = "Картина \"Спокойствие\"",
                            PaintingTechnique = "масло",
                            SizeId = 1,
                            Year = 2023
                        },
                        new
                        {
                            Id = 2,
                            Cost = 8000m,
                            Description = "Парная картина",
                            Genre = 3,
                            IsPromo = false,
                            Name = "Картина \"Орлы\"",
                            PaintingTechnique = "масло",
                            SizeId = 2,
                            Year = 2022
                        },
                        new
                        {
                            Id = 3,
                            Cost = 3000m,
                            Description = "Картина для интерьера",
                            Genre = 2,
                            IsPromo = true,
                            Name = "Картина \"Бокал вина\"",
                            PaintingTechnique = "масло",
                            SizeId = 3,
                            Year = 2022
                        },
                        new
                        {
                            Id = 4,
                            Cost = 8000m,
                            Description = "Осенний пейзаж",
                            Genre = 0,
                            IsPromo = false,
                            Name = "Картина \"Золотая осень\"",
                            PaintingTechnique = "масло",
                            SizeId = 4,
                            Year = 2022
                        },
                        new
                        {
                            Id = 5,
                            Cost = 5000m,
                            Description = "Тиранозавр Рекс",
                            Genre = 3,
                            IsPromo = true,
                            Name = "Картина \"Динозавр\"",
                            PaintingTechnique = "масло",
                            SizeId = 5,
                            Year = 2022
                        },
                        new
                        {
                            Id = 6,
                            Cost = 4000m,
                            Description = "Картина в подарок подруге",
                            Genre = 1,
                            IsPromo = false,
                            Name = "Картина \"Девушка и ветер\"",
                            PaintingTechnique = "масло",
                            SizeId = 2,
                            Year = 2021
                        },
                        new
                        {
                            Id = 7,
                            Cost = 6000m,
                            Description = "Клоун из фильма \"Оно\"",
                            Genre = 1,
                            IsPromo = false,
                            Name = "Картина \"Пеннивайз\"",
                            PaintingTechnique = "масло",
                            SizeId = 6,
                            Year = 2022
                        },
                        new
                        {
                            Id = 8,
                            Cost = 5500m,
                            Description = "Картина в подарок подростку",
                            Genre = 1,
                            IsPromo = false,
                            Name = "Картина \"Депрессия\"",
                            PaintingTechnique = "масло",
                            SizeId = 7,
                            Year = 2022
                        },
                        new
                        {
                            Id = 9,
                            Cost = 2800m,
                            Description = "Анджелина Джоли в роли Лары Крофт",
                            Genre = 1,
                            IsPromo = false,
                            Name = "Картина \"Лара Крофт\"",
                            PaintingTechnique = "масло",
                            SizeId = 2,
                            Year = 2021
                        },
                        new
                        {
                            Id = 10,
                            Cost = 12000m,
                            Description = "Олени на водопое",
                            Genre = 3,
                            IsPromo = true,
                            Name = "Картина \"Утро\"",
                            PaintingTechnique = "масло",
                            SizeId = 1,
                            Year = 2023
                        },
                        new
                        {
                            Id = 11,
                            Cost = 6000m,
                            Description = "Пейзаж со струящейся рекой",
                            Genre = 0,
                            IsPromo = true,
                            Name = "Картина \"Лето\"",
                            PaintingTechnique = "масло",
                            SizeId = 1,
                            Year = 2023
                        },
                        new
                        {
                            Id = 12,
                            Cost = 10000m,
                            Description = "Картина в кабинет",
                            Genre = 2,
                            IsPromo = false,
                            Name = "Картина \"Мудрость\"",
                            PaintingTechnique = "масло",
                            SizeId = 5,
                            Year = 2023
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<bool>("IsFrame")
                        .HasColumnType("bit");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sizes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Height = 30,
                            IsFrame = true,
                            Width = 20
                        },
                        new
                        {
                            Id = 2,
                            Height = 25,
                            IsFrame = false,
                            Width = 20
                        },
                        new
                        {
                            Id = 3,
                            Height = 25,
                            IsFrame = true,
                            Width = 20
                        },
                        new
                        {
                            Id = 4,
                            Height = 60,
                            IsFrame = true,
                            Width = 50
                        },
                        new
                        {
                            Id = 5,
                            Height = 35,
                            IsFrame = false,
                            Width = 30
                        },
                        new
                        {
                            Id = 6,
                            Height = 30,
                            IsFrame = false,
                            Width = 25
                        },
                        new
                        {
                            Id = 7,
                            Height = 15,
                            IsFrame = false,
                            Width = 20
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.BasketItem", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Basket", "Basket")
                        .WithMany("BasketItems")
                        .HasForeignKey("BasketId");

                    b.HasOne("OnlineShop.Db.Models.Order", "Order")
                        .WithMany("OrderBasketItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("BasketItems")
                        .HasForeignKey("ProductId");

                    b.Navigation("Basket");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Comparison", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Favourites", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.FilePath", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("ImagePath")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Contacts", "Contacts")
                        .WithMany()
                        .HasForeignKey("ContactsId");

                    b.Navigation("Contacts");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Size", "Size")
                        .WithMany("Products")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Size");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Basket", b =>
                {
                    b.Navigation("BasketItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Navigation("OrderBasketItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Navigation("BasketItems");

                    b.Navigation("ImagePath");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Size", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
