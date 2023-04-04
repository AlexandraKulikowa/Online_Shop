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
        public Basket TryGetByBasketId(Guid basketId)
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
        public void ChangeAmount(int id, Guid basketid, bool sign)
        {
            var basket = TryGetByBasketId(basketid); // впоследствии все basketid заменю на userid
            var ProductForChange = new BasketItem();

            foreach (BasketItem item in basket.ProductsInBasket)
            {
                if (item.Product.Id == id)
                    ProductForChange = item;
            }

            if (sign == false && ProductForChange.Amount == 1)
            {
                ClearBasketItem(basketid, id);
            }
            else
            {
                ProductForChange.ChangeAmount(sign);
            }
        }
        public void ClearBasketItem(Guid basketId, int id)
        {
            var basket = TryGetByBasketId(basketId);
            var basketitem = basket.ProductsInBasket.FirstOrDefault(x => x.Product.Id == id);
            basket.ProductsInBasket.Remove(basketitem);
        }
        public void ClearBasket(Guid basketId)
        {
            var basket = TryGetByBasketId(basketId);
            basket.ProductsInBasket.Clear();
        }
    }
}