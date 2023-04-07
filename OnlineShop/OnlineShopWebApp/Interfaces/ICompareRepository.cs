using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp.Interfaces
{
    public interface ICompareRepository
    {
        List<Product> ProductsForCompare { get; }
        Product TryGetById(int id);
        void Add(Product product);
        void DeleteProduct(int id);
        void Clear();
    }
}
