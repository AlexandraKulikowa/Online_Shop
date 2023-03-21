﻿namespace OnlineShopWebApp.Models
{
    public class Product
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string PaintingTechnique { get; set; }
        public string Size { get; set; }
        public int Year { get; set; }
        public bool IsPromo { get; set; }
    }
}
