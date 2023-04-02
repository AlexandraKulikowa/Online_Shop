using System.Collections.Generic;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> ListProducts { get; }
        Product TryGetById(int id);
    }
}
