using System;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public class BasketItemViewModel
    {
        public int Id { get; set; }
        public ProductViewModel Product { get; set; }
        public int Amount { get; set; }
        public decimal Cost
        {
            get
            {
                return Product.Cost * Amount;
            }
        }
        public BasketItemViewModel() { }
        public BasketItemViewModel(ProductViewModel product, int amount)
        {
            Product = product;
            Amount = amount;
        }
        public void ChangeAmount(bool sign)
        {
            if (sign)
                Amount++;
            else
                Amount--;
        }
    }
}