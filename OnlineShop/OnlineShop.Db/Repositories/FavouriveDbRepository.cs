using System.Linq;
using OnlineShop.Db.Models;
using OnlineShop.Db.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace OnlineShop.Db.Repositories
{
    public class FavouritesDbRepository : IFavouriteRepository
    {
        private readonly DatabaseContext databaseContext;

        public FavouritesDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Favourites> TryGetByIdAsync(string userId, int id)
        {
            return await databaseContext.Favorites
                .Include(x => x.Product)
                    .ThenInclude(x => x.Size)
                .Include(x => x.Product)
                    .ThenInclude(x => x.ImagePath)
                .FirstOrDefaultAsync(x => x.UserId == userId & x.Product.Id == id);
        }

        public async Task<List<Product>> GetAllAsync(string userId)
        {
            var products = await databaseContext.Favorites
                .Include(x => x.Product)
                    .ThenInclude(x => x.Size)
                .Include(x => x.Product)
                    .ThenInclude(x => x.ImagePath)
                .Where(x => x.UserId == userId)
                .Select(x => x.Product)
                .ToListAsync();
            return products;
        }

        public async Task AddAsync(Product product, string userId)
        {
            var existingFavourite = await TryGetByIdAsync(userId, product.Id);

            if (existingFavourite == null)
            {
                var newList = new Favourites
                {
                    UserId = userId,
                    Product = product
                };

                await databaseContext.Favorites.AddAsync(newList);
                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task DeleteFavouriteAsync(string userId, int id)
        {
            var favourite = await TryGetByIdAsync(userId, id);
            databaseContext.Favorites.Remove(favourite);
            await databaseContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            var list = await databaseContext.Favorites.Where(x => x.UserId == userId).ToListAsync();
            databaseContext.Favorites.RemoveRange(list);
            await databaseContext.SaveChangesAsync();
        }
    }
}