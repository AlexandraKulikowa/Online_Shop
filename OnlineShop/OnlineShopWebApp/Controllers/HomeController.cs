using System;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private static ProductList products;
        public HomeController()
        {
            products = new ProductList();
        }
        public string Index()
        {
            var result = String.Join("\n", products.GetAll());
            return result;
        }
    }
}
