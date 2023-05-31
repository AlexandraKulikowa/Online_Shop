using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.IO;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Comparison> Comparisons { get; set; }
        public DbSet<Favourites> Favorites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<FilePath> Files { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasData(
                    new Product[]
                    {
                        new Product { Id = 1, Name = "Картина \"Спокойствие\"", Cost = 10000, Description = "Летний пейзаж", Genre = Genre.Пейзаж, PaintingTechnique = "масло", SizeId = 1, Year = 2023, IsPromo = false },
                        new Product { Id = 2, Name = "Картина \"Орлы\"", Cost = 8000, Description = "Парная картина", Genre = Genre.Анималистика, PaintingTechnique = "масло", SizeId = 2, Year = 2022, IsPromo = false},
                        new Product { Id = 3, Name = "Картина \"Бокал вина\"", Cost = 3000, Description = "Картина для интерьера", Genre = Genre.Натюрморт, PaintingTechnique = "масло", SizeId = 3, Year = 2022, IsPromo = true },
                        new Product { Id = 4, Name = "Картина \"Золотая осень\"", Cost = 8000, Description = "Осенний пейзаж", Genre = Genre.Пейзаж, PaintingTechnique = "масло", SizeId = 4, Year = 2022, IsPromo = false },
                        new Product { Id = 5, Name = "Картина \"Динозавр\"", Cost = 5000, Description = "Тиранозавр Рекс", Genre = Genre.Анималистика, PaintingTechnique = "масло", SizeId = 5, Year = 2022, IsPromo = true },
                        new Product { Id = 6, Name = "Картина \"Девушка и ветер\"", Cost = 4000, Description = "Картина в подарок подруге", Genre = Genre.Портрет, PaintingTechnique = "масло", SizeId = 2, Year = 2021, IsPromo = false },
                        new Product { Id = 7, Name = "Картина \"Пеннивайз\"", Cost = 6000, Description = "Клоун из фильма \"Оно\"", Genre = Genre.Портрет, PaintingTechnique = "масло", SizeId = 6, Year = 2022, IsPromo = false },
                        new Product { Id = 8, Name = "Картина \"Депрессия\"", Cost = 5500, Description = "Картина в подарок подростку", Genre = Genre.Портрет, PaintingTechnique = "масло", SizeId = 7, Year = 2022, IsPromo = false },
                        new Product { Id = 9, Name = "Картина \"Лара Крофт\"", Cost = 2800, Description = "Анджелина Джоли в роли Лары Крофт", Genre = Genre.Портрет, PaintingTechnique = "масло", SizeId = 2, Year = 2021, IsPromo = false }
                    });


            modelBuilder.Entity<FilePath>()
                .HasData(new
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
                    });


            modelBuilder.Entity<Size>()
                .HasData(
        new Size[]
        {
                        new Size { Id = 1, Width = 20, Height = 30, IsFrame = true },
                        new Size { Id = 2, Width = 20, Height = 25, IsFrame = false },
                        new Size { Id = 3, Width = 20, Height = 25, IsFrame = true },
                        new Size { Id = 4, Width = 50, Height = 60, IsFrame = true },
                        new Size { Id = 5, Width = 30, Height = 35, IsFrame = false },
                        new Size { Id = 6, Width = 25, Height = 30, IsFrame = false },
                        new Size { Id = 7, Width = 20, Height = 15, IsFrame = false }
        });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Property(p => p.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}