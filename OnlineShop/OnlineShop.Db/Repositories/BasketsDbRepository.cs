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
            return databaseContext.Baskets.Include(x => x.BasketItems).ThenInclude(x => x.Product).ThenInclude(x => x.Size).FirstOrDefault(x => x.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var existingBasket = TryGetByUserId(userId);
            if (existingBasket == null)
            {
                var newBasket = new Basket
                {
                    UserId = userId
                };

                newBasket.BasketItems = new List<BasketItem>
                    {
                        new BasketItem
                        {
                            Product = product,
                            Amount = 1,
                            Basket = newBasket
                        }
                    };

                databaseContext.Baskets.Add(newBasket);
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
                        Amount = 1,
                        Basket = existingBasket
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
                ChangeAmountInBasketItem(sign, ProductForChange);
            }
            databaseContext.SaveChanges();
        }

        public void ChangeAmountInBasketItem(bool sign, BasketItem ProductForChange)
        {
            if (sign)
                ProductForChange.Amount++;
            else
                ProductForChange.Amount--;
        }

        public void ClearItem(string userId, int id)
        {
            var basket = TryGetByUserId(userId);
            var basketitem = basket.BasketItems.FirstOrDefault(x => x.Product.Id == id);
            databaseContext.BasketItems.Remove(basketitem);
            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var basket = TryGetByUserId(userId);
            databaseContext.Baskets.Remove(basket);
            databaseContext.SaveChanges();
        }
    }
}