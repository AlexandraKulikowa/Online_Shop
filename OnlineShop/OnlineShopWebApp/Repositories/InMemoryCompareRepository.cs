using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Repositories
{
    public class InMemoryCompareRepository : ICompareRepository
    {
        private List<Comparison> compareList = new List<Comparison>();
        public List<Comparison> CompareList => compareList;
        public Comparison TryGetByUserId(string userId)
        {
            return compareList.FirstOrDefault(x => x.UserId == userId);
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