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
        public Basket TryGetById(Guid basketId)
        {
            return baskets.FirstOrDefault(x => x.Id == basketId);
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
        public void ChangeAmount(int id, Guid basketid, bool sign, string userId)
        {
            var existingBasket = TryGetByUserId(userId);
            if (existingBasket == null)
            { return; }

            var basket = TryGetById(basketid);
            var ProductForChange = new BasketItem();

            foreach (BasketItem item in basket.ProductsInBasket)
            {
                if (item.Product.Id == id)
                    ProductForChange = item;
            }

            if (!sign && ProductForChange.Amount == 1)
            {
                ClearItem(basketid, id);
            }
            else
            {
                ProductForChange.ChangeAmount(sign);
            }
        }
        public void ClearItem(Guid basketId, int id)
        {
            var basket = TryGetById(basketId);
            var basketitem = basket.ProductsInBasket.FirstOrDefault(x => x.Product.Id == id);
            basket.ProductsInBasket.Remove(basketitem);
        }
        public void Clear(Guid basketId)
        {
            var basket = TryGetById(basketId);
            basket.ProductsInBasket.Clear();
        }
    }
}