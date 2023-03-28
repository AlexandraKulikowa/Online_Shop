﻿namespace OnlineShopWebApp.Models
{
    public class Product
    {
        private static int counter = 1;
        public int Id { get; }
        public string Name { get; }
        public decimal Cost { get; }
        public string Description { get; }
        public GenreEnum Genre { get; }
        public string PaintingTechnique { get; }
        public Size Size { get; }
        public int Year { get; }
        public bool IsPromo { get; }
        public Product() {}
        public Product(string name, decimal cost, string description, GenreEnum genre, string paintingTechnique, Size size, int year, bool ispromo)
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
            counter++;
        }
        public override string ToString()
        {
            return $"{Id} \n{Name} \n{Cost}";
        }
    }
}