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
        void ClearBasket(Guid basketId);
    }
}