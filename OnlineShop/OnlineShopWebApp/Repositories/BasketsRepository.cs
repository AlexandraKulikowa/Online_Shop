using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public class BasketsRepository : IBasketRepository
    {
        static List<Basket> baskets;
        public BasketsRepository()
        {
            baskets = new List<Basket>();
        }
        public List<Basket> Baskets
        {
            get
            {
                return baskets;
            }
        }
        public Basket TryGetByUserId(string userId)
        {
            return baskets.FirstOrDefault(x => x.UserId == userId);
        }
        public void Add(Product product, string userId)
        {
            var existingBasket = TryGetByUserId(userId);
            if (existingBasket == null)
            {
                var newBasket = new Basket
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ProductsInBasket = new List<BasketItem>
                    {
                        new BasketItem
                        {
                            Id = Guid.NewGuid(),
                            Product = product,
                            Amount = 1
                        }
                    }
                };
                baskets.Add(newBasket);
            }
            else
            {
                var existingBasketItem = existingBasket.ProductsInBasket.FirstOrDefault(x => x.Product.Id == product.Id);
                if (existingBasketItem != null)
                {
                    existingBasketItem.Amount += 1;
                }
                else
                {
                    existingBasket.ProductsInBasket.Add(new BasketItem
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        Amount = 1
                    });
                }
            }
        }
    }
}