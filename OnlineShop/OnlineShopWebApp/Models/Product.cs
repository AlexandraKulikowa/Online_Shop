using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Product
    {
        private static int counter = 1;
        public int Id { get; set; }

        [Required(ErrorMessage = "Вы не ввели название товара")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Введите полное название товара")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вы не ввели цену товара")]
        [Range(100, 5000000, ErrorMessage = "Введите верную цену товара")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Вы не ввели описание товара")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Введите описание товара")]
        public string Description { get; set; }

        public GenreEnum Genre { get; set; }

        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Неверное значение")]
        [Required(ErrorMessage = "Вы не ввели технику написания картины")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Введите верное значение")]
        public string PaintingTechnique { get; set; }

        public Size Size { get; set; }

        [Required(ErrorMessage = "Это неверный год создания!")]
        [Range(2015, 2023, ErrorMessage = "Введите корректное значение")]
        public int Year { get; set; }

        public bool IsPromo { get; set; }

        [RegularExpression(@"^((~)?)(\/.*\/)(?!\.\.$|\.$)([^\/]+)$", ErrorMessage = "Неверное значение")]
        [StringLength(35, MinimumLength = 8, ErrorMessage = "Введите верное значение")]
        public string ImagePath { get; set; }

        public Product()
        {
            Id = counter;
            counter++;
        }
        public Product(string name, decimal cost, string description, GenreEnum genre, string paintingTechnique, Size size, int year, bool ispromo, string imagePath)
        {
            Id = counter;
            Name = name;
            Cost = cost;
            Description = description;
            Genre = genre;
            PaintingTechnique = paintingTechnique;
            Size = size;
            Year = year;
            IsPromo = ispromo;
            ImagePath = imagePath;
            counter++;
        }
        public override string ToString()
        {
            return $"{Id} \n{Name} \n{Cost}";
        }
    }
}