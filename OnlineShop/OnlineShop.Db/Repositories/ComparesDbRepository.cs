using System.Collections.Generic;
using System.Linq;
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

        public Comparison TryGetById(string userId, int id)
        {
            return databaseContext.Comparisons
                .Include(x => x.Product)
                    .ThenInclude(x => x.Size)
                .Include(x => x.Product)
                    .ThenInclude(x => x.ImagePath)
                .FirstOrDefault(x => x.UserId == userId & x.Product.Id == id);
        }

        public List<Product> GetAll(string userId)
        {
            var products = databaseContext.Comparisons
                .Include(x => x.Product)
                    .ThenInclude(x => x.Size)
                .Include(x => x.Product)
                    .ThenInclude(x => x.ImagePath)
                .Where(x => x.UserId == userId)
                .Select(x => x.Product)
                .ToList();
            return products;
        }

        public void Add(Product product, string userId)
        {
            var comparison = TryGetById(userId, product.Id);

            if (comparison == null)
            {
                var newComparison = new Comparison
                {
                    UserId = userId,
                    Product = product
                };
                databaseContext.Comparisons.Add(newComparison);
                databaseContext.SaveChanges();
            }
        }

        public void DeleteProduct(string userId, int id)
        {
            var comparison = TryGetById(userId, id);
            databaseContext.Comparisons.Remove(comparison);
            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var list = databaseContext.Comparisons.Where(x => x.UserId == userId).ToList();
            databaseContext.Comparisons.RemoveRange(list);
            databaseContext.SaveChanges();
        }
    }
}