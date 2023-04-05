using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Repositories;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketRepository baskets;
        private readonly IOrderRepository orders;
        public OrderController(IBasketRepository baskets, IOrderRepository orders)
        {
            this.baskets = baskets;
            this.orders = orders;
        }
        public IActionResult Index(Guid basketId)
        {
            var basketById = baskets.TryGetByBasketId(basketId);
            return View(basketById);
        }
        public IActionResult ToCheckOut(Guid basketId)
        {
            var basketById = baskets.TryGetByBasketId(basketId);
            orders.Add(basketById);
            baskets.Clear(basketId);
            return RedirectToAction("Index");
        }
    }
}
