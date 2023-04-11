using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories;
using System.ComponentModel.DataAnnotations;

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
        public IActionResult Index()
        {
            var basketById = baskets.TryGetByUserId(Constants.UserId);
            return View(basketById);
        }

        [HttpPost]
        [DataType(DataType.Date)]
        public IActionResult ToCheckOut(Order order)
        {
            var basket = baskets.TryGetByUserId(Constants.UserId);
            order.Basket = basket;
            orders.Add(order);
            baskets.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}