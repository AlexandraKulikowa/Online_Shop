using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp.Interfaces
{
    public interface IBasketRepository
    {
        List<Basket> Baskets { get; }
        Basket TryGetByUserId(string userId);
        void Add(Product product, string userId);
    }
}
