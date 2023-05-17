using OnlineShop.Db.Models;
using System.Collections.Generic;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        Product TryGetById(int id);
        List<Product> Search(string name);
        void Add(Product product);
        void Edit(Product product);
        void Delete(Product product);
        bool CheckNewProduct(Product product);
        List<Product> SortByGenre(Genre genre);
        List<Product> SortByCost(string sortOrder);
    }
}