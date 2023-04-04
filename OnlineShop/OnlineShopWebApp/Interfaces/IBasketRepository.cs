using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Interfaces
{
    public interface IBasketRepository
    {
        List<Basket> Baskets { get; }
        Basket TryGetByUserId(string userId);
        Basket TryGetByBasketId(Guid basketId);
        void Add(Product product, string userId);
        void ChangeAmount(int id, Guid basketid, bool sign);
        void ClearBasketItem(Guid basketId, int id);
        void ClearBasket(Guid basketId);
    }
}