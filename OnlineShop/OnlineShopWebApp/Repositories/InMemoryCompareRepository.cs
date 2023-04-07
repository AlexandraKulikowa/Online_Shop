using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Repositories
{
    public class InMemoryCompareRepository : ICompareRepository
    {
        private List<Product> productsForCompare = new List<Product>();
        public List<Product> ProductsForCompare
        {
            get
            {
                return productsForCompare;
            }
        }
        public Product TryGetById(int id)
        {
            return productsForCompare.FirstOrDefault(product => product.Id == id);
        }
        public void Add(Product product)
        {
            var existingProduct = TryGetById(product.Id);
            if (existingProduct == null)
            {
                productsForCompare.Add(product);
            }
        }
        public void DeleteProduct(int id)
        {
            productsForCompare.Remove(TryGetById(id));
        }
        public void Clear()
        {
            productsForCompare.Clear();
        }
    }
}
