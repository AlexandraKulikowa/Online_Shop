using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class BasketsDbRepository : IBasketsRepository
    {
        private readonly DatabaseContext databaseContext;

        public BasketsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Basket TryGetByUserId(string userId)
        {
            return databaseContext.Baskets.Include(x => x.BasketItems).FirstOrDefault(x => x.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var existingBasket = TryGetByUserId(userId);
            if (existingBasket == null)
            {
                var newBasket = new Basket
                {
                    UserId = userId,
                    BasketItems = new List<BasketItem>
                    {
                        new BasketItem
                        {
                            Product = product,
                            Amount = 1
                        }
                    }
                };
                databaseContext.Baskets.Add(newBasket);
                databaseContext.SaveChanges();
            }
            else
            {
                var existingBasketItem = existingBasket.BasketItems.FirstOrDefault(x => x.Product.Id == product.Id);
                if (existingBasketItem != null)
                {
                    existingBasketItem.Amount += 1;
                }
                else
                {
                    existingBasket.BasketItems.Add(new BasketItem
                    {
                        Product = product,
                        Amount = 1
                    });
                }
            }
            databaseContext.SaveChanges();
        }
        public void ChangeAmount(int id, bool sign, string userId)
        {
            var existingBasket = TryGetByUserId(userId);
            if (existingBasket == null)
            { return; }

            var ProductForChange = new BasketItem();

            foreach (BasketItem item in existingBasket.BasketItems)
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
            databaseContext.SaveChanges();
        }
        public void ClearItem(string userId, int id)
        {
            var basket = TryGetByUserId(userId);
            var basketitem = basket.BasketItems.FirstOrDefault(x => x.Product.Id == id);
            databaseContext.Remove(basketitem);
            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var basket = TryGetByUserId(userId);
            basket.BasketItems.Clear();
            databaseContext.SaveChanges();
        }
    }
}