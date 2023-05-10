using System.Collections.Generic;
using System.Linq;
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

        public Comparison TryGetByUserId(string userId)
        {
            return databaseContext.FirstOrDefault(x => x.UserId == userId);
        }
        public void Add(Product product, string userId)
        {
            var existingList = TryGetByUserId(userId);
            if (existingList == null)
            {
                var newList = new Comparison
                {
                    UserId = userId,
                    Products = new List<Product> { product }
                };
                compareList.Add(newList);
            }
            else
            {
                var existingProduct = existingList.Products.FirstOrDefault(x => x.Id == product.Id);
                if (existingProduct == null)
                {
                    existingList.Products.Add(product);
                }
            }
        }
        public void DeleteProduct(string userId, int id)
        {
            var list = TryGetByUserId(userId);
            var product = list.Products.FirstOrDefault(x => x.Id == id);
            list.Products.Remove(product);
        }
        public void Clear(string userId)
        {
            var list = TryGetByUserId(userId);
            list.Products.Clear();
        }
    }
}