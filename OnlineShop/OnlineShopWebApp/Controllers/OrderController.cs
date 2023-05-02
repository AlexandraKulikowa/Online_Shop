using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Repositories;
using System;
using System.Linq;

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
            ViewBag.Basket = basketById.ProductsInBasket.Any();
            ViewBag.TotalCost = basketById.TotalCost();
            return View();
        }

        [HttpPost]
        public IActionResult ToCheckOut(Order order)
        {
            if(ModelState.IsValid)
            {
                var basket = baskets.TryGetByUserId(Constants.UserId);
                order.Products.AddRange(basket.ProductsInBasket.ToArray());
                orders.Add(order);
                baskets.Clear(Constants.UserId);
                return RedirectToAction("Result", order);
            }
            return View(order);
        }
        public IActionResult Result(Order order)
        {
            return View(order);
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckDate(DateTime DateofDelivery)
        {
            TimeSpan diff = DateofDelivery.Subtract(DateTime.Now);
            if(diff.TotalDays > 0)
                return Json(true);
            return Json(false);
        }
    }
}