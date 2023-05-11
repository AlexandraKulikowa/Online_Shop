using OnlineShop.Db.Models;
using System.Collections.Generic;

namespace OnlineShop.Db.Interfaces
{
    public interface IFavouriteRepository
    {
        Favourites TryGetById(string userId, int id);
        List<Product> GetAll(string userId);
        void Add(Product product, string userId);
        void DeleteFavourite(string userId, int id);
        void Clear(string userId);
    }
}