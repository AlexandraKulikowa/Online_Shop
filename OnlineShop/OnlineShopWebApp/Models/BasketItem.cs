using System;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public decimal Cost
        {
            get
            {
                return Product.Cost * Amount;
            }
        }
        public BasketItem(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }
    }
}