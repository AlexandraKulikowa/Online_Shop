using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopWebApp.Models

{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вы не ввели название товара")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Введите полное название товара")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вы не ввели цену товара")]
        [Range(100, 5000000, ErrorMessage = "Введите верную цену товара")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Вы не ввели описание товара")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Введите описание товара")]
        public string Description { get; set; }

        public Genre Genre { get; set; }

        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Неверное значение")]
        [Required(ErrorMessage = "Вы не ввели технику написания картины")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Введите верное значение")]
        public string PaintingTechnique { get; set; }

        public SizeViewModel Size { get; set; }

        [Required(ErrorMessage = "Это неверный год создания!")]
        [Range(2015, 2023, ErrorMessage = "Введите корректное значение")]
        public int Year { get; set; }

        public bool IsPromo { get; set; }

        public string ImagePath { get; set; }

        public IFormFile? UploadedFile { get; set; }
    }
}