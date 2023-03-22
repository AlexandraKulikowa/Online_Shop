﻿
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OnlineShopWebApp.Models
{
    public class Product
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum GenreEnum 
        { 
            Landscape, 
            Portrait, 
            StillLife, 
            Animalism
        }

        private static int counter = 1;
        public int Id { get; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public GenreEnum Genre { get; set; }
        public string PaintingTechnique { get; set; }
        public Size Size { get; set; }
        public int Year { get; set; }
        public bool IsPromo { get; set; }

        public Product(string name, decimal cost, string description, GenreEnum genre, string paintingTechnique, Size size, int year, bool ispromo)
        {
            Id = counter++;
            Name = name;
            Cost = cost;
            Description = description;
            Genre = genre;
            PaintingTechnique = paintingTechnique;
            Size = size;
            Year = year;
            IsPromo = ispromo;
        }
        public override string ToString() 
        {
            return $"{Id} \n{Name} \n{Cost} \n\n";
        }
}
}
