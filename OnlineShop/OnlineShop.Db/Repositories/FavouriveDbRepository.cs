using System.Linq;
using OnlineShop.Db.Models;
using OnlineShop.Db.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Db.Repositories
{
    public class FavouritesDbRepository : IFavouriteRepository
    {
        private readonly DatabaseContext databaseContext;

        public FavouritesDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Favourites TryGetById(string userId, int id)
        {
            return databaseContext.Favorites
                .Include(x => x.Product)
                .ThenInclude(x => x.Size)
                .FirstOrDefault(x => x.UserId == userId & x.Product.Id == id);
        }

        public List<Product> GetAll(string userId)
        {
            var products = databaseContext.Favorites
                .Include(x => x.Product)
                .ThenInclude(x => x.Size)
                .Where(x => x.UserId == userId)
                .Select(x => x.Product)
                .ToList();
            return products;
        }

        public void Add(Product product, string userId)
        {
            var existingFavourite = TryGetById(userId, product.Id);

            if (existingFavourite == null)
            {
                var newList = new Favourites
                {
                    UserId = userId,
                    Product = product
                };

                databaseContext.Favorites.Add(newList);
                databaseContext.SaveChanges();
            }
        }

        public void DeleteFavourite(string userId, int id)
        {
            var favourite = TryGetById(userId, id);
            databaseContext.Favorites.Remove(favourite);
            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var list = databaseContext.Favorites.Where(x => x.UserId == userId).ToList();
            databaseContext.Favorites.RemoveRange(list);
            databaseContext.SaveChanges();
        }
    }
}