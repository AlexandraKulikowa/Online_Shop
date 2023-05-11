using System.Collections.Generic;

namespace OnlineShop.Db.Models

{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }

        public Genre Genre { get; set; }

        public string PaintingTechnique { get; set; }

        public Size Size { get; set; }

        public int SizeId { get; set; }

        public int Year { get; set; }

        public bool IsPromo { get; set; }

        public string ImagePath { get; set; }

        public List<BasketItem> BasketItems { get; set; }
        
        public Product()
        {
            BasketItems = new List<BasketItem>();
        }

    }
}