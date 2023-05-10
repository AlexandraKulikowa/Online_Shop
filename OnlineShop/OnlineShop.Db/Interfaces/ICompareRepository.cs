using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShop.Db.Interfaces
{
    public interface ICompareRepository
    {
        List<Comparison> CompareList { get; }
        Comparison TryGetByUserId(string userId);
        void Add(ProductViewModel product, string userId);
        void DeleteProduct(string userId, int id);
        void Clear(string userId);
    }
}