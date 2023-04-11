using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Repositories;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly IProductsRepository products;
        private readonly IBasketRepository baskets;
        public BasketController(IProductsRepository products, IBasketRepository baskets)
        {
            this.products = products;
            this.baskets = baskets;
        }
        public IActionResult Index()
        {
            var basket = baskets.TryGetByUserId(Constants.UserId);
            return View(basket);
        }
        public IActionResult Add(int ProductId)
        {
            var product = products.TryGetById(ProductId);
            baskets.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult ChangeAmount(int id, bool sign)
        {
            baskets.ChangeAmount(id, sign, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            baskets.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}