using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class ComparesDbRepository : ICompareRepository
    {
        private readonly DatabaseContext databaseContext;

        public ComparesDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Comparison> TryGetByIdAsync(string userId, int id)
        {
            return await databaseContext.Comparisons
                .Include(x => x.Product)
                    .ThenInclude(x => x.Size)
                .Include(x => x.Product)
                    .ThenInclude(x => x.ImagePath)
                .FirstOrDefaultAsync(x => x.UserId == userId & x.Product.Id == id);
        }

        public async Task<List<Product>> GetAllAsync(string userId)
        {
            var products = await databaseContext.Comparisons
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
            var comparison = await TryGetByIdAsync(userId, product.Id);

            if (comparison == null)
            {
                var newComparison = new Comparison
                {
                    UserId = userId,
                    Product = product
                };
                await databaseContext.Comparisons.AddAsync(newComparison);
                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(string userId, int id)
        {
            var comparison = await TryGetByIdAsync(userId, id);
            databaseContext.Comparisons.Remove(comparison);
            await databaseContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            var list = await databaseContext.Comparisons.Where(x => x.UserId == userId).ToListAsync();
            databaseContext.Comparisons.RemoveRange(list);
            await databaseContext.SaveChangesAsync();
        }
    }
}