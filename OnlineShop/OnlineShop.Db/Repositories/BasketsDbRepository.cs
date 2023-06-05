using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Basket> TryGetByUserIdAsync(string userId)
        {
            return await databaseContext.Baskets
                .Include(x => x.BasketItems)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.ImagePath)
                .Include(x => x.BasketItems)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Size)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task AddAsync(Product product, string userId)
        {
            var existingBasket = await TryGetByUserIdAsync(userId);
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

                await databaseContext.Baskets.AddAsync(newBasket);
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
            await databaseContext.SaveChangesAsync();
        }

        public async Task ChangeAmountAsync(int id, bool sign, string userId)
        {
            var existingBasket = await TryGetByUserIdAsync(userId);
            if (existingBasket == null)
            { return; }

            var product = new BasketItem();

            foreach (BasketItem item in existingBasket.BasketItems)
            {
                if (item.Product.Id == id)
                    product = item;
            }

            if (!sign && product.Amount == 1)
            {
                await ClearItemAsync(userId, id);
            }
            else
            {
                ChangeAmountInBasketItem(sign, product);
            }
            await databaseContext.SaveChangesAsync();
        }

        public void ChangeAmountInBasketItem(bool sign, BasketItem ProductForChange)
        {
            if (sign)
                ProductForChange.Amount++;
            else
                ProductForChange.Amount--;
        }

        public async Task ClearItemAsync(string userId, int id)
        {
            var basket = await TryGetByUserIdAsync(userId);
            var basketitem = basket.BasketItems.FirstOrDefault(x => x.Product.Id == id);
            databaseContext.BasketItems.Remove(basketitem);
            await databaseContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            var basket = await TryGetByUserIdAsync(userId);
            databaseContext.Baskets.Remove(basket);
            await databaseContext.SaveChangesAsync();
        }
    }
}