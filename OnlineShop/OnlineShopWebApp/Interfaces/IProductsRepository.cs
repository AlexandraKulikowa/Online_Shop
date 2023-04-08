﻿using System.Collections.Generic;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        Product TryGetById(int id);
    }
}
