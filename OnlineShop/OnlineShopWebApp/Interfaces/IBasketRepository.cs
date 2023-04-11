using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Interfaces
{
    public interface IBasketRepository
    {
        List<Basket> Baskets { get; }
        Basket TryGetByUserId(string userId);
        Basket TryGetById(Guid basketId);
        void Add(Product product, string userId);
        void ChangeAmount(int id, Guid basketid, bool sign, string userId);
        void ClearItem(Guid basketId, int id);
        void Clear(Guid basketId);
    }
}