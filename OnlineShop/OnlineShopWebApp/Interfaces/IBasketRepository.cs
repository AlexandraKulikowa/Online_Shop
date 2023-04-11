using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Interfaces
{
    public interface IBasketRepository
    {
        List<Basket> Baskets { get; }
        Basket TryGetByUserId(string userId);
        void Add(Product product, string userId);
        void ChangeAmount(int id, bool sign, string userId);
        void ClearItem(string userId, int id);
        void Clear(string userId);
    }
}