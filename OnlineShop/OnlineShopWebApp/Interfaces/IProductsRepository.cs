using System.Collections.Generic;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        Product TryGetById(int id);
        List<Product> Search(string name);
        void Add(Product product);
        void Edit(Product product);
        void Delete(Product product);
    }
}