using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public class InMemoryBasketsRepository : IBasketRepository
    {
        private List<Basket> baskets = new List<Basket>();
        public List<Basket> Baskets => baskets;
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
                Baskets.Add(newBasket);
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
        public void ChangeAmount(int id, bool sign, string userId)
        {
            var existingBasket = TryGetByUserId(userId);
            if (existingBasket == null)
            { return; }

            var ProductForChange = new BasketItem();

            foreach (BasketItem item in existingBasket.ProductsInBasket)
            {
                if (item.Product.Id == id)
                    ProductForChange = item;
            }

            if (!sign && ProductForChange.Amount == 1)
            {
                ClearItem(userId, id);
            }
            else
            {
                ProductForChange.ChangeAmount(sign);
            }
        }
        public void ClearItem(string userId, int id)
        {
            var basket = TryGetByUserId(userId);
            var basketitem = basket.ProductsInBasket.FirstOrDefault(x => x.Product.Id == id);
            basket.ProductsInBasket.Remove(basketitem);
        }
        public void Clear(string userId)
        {
            var basket = TryGetByUserId(userId);
            basket.ProductsInBasket.Clear();
        }
    }
}