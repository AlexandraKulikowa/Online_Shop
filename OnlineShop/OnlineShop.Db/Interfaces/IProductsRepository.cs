using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> TryGetByIdAsync(int id);
        Task<List<Product>> SearchAsync(string name);
        Task AddAsync(Product product);
        Task EditAsync(Product product);
        Task DeleteAsync(Product product);
        bool CheckNewProduct(Product product);
        Task<List<Product>> GetByGenreAsync(Genre genre);
    }
}