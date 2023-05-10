using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface ICompareRepository
    {
        Comparison TryGetByUserId(string userId);
        void Add(Product product, string userId);
        void DeleteProduct(string userId, int id);
        void Clear(string userId);
    }
}