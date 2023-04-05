using System.Collections.Generic;

namespace OnlineShopWebApp.Interfaces
{
    public interface IOrderRepository
    {
        List<Basket> Orders { get; }
        void Add(Basket basket);
    }
}
