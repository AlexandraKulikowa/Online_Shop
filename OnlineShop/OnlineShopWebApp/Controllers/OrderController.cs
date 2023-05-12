using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Repositories;
using System;
using System.Linq;

namespace OnlineShopWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketsRepository baskets;
        private readonly IOrderRepository orders;
        public OrderController(IBasketsRepository baskets, IOrderRepository orders)
        {
            this.baskets = baskets;
            this.orders = orders;
        }
        public IActionResult Index()
        {
            var basket = baskets.TryGetByUserId(Constants.UserId);
            var basketVM = Mapping.ToBasketViewModel(basket);
            ViewBag.Basket = basketVM.BasketItems.Any();
            ViewBag.TotalCost = basketVM.TotalCost();
            return View();
        }

        [HttpPost]
        public IActionResult ToCheckOut(OrderViewModel orderVM)
        {
            if (ModelState.IsValid)
            {
                var basket = baskets.TryGetByUserId(Constants.UserId);
                var basketVM = Mapping.ToBasketViewModel(basket);
                orderVM.Products.AddRange(basketVM.BasketItems.ToArray());
                var order = Mapping.ToOrder(orderVM);
                orders.Add(order, Constants.UserId);
                baskets.Clear(Constants.UserId);
                return RedirectToAction("Result", orderVM);
            }
            return View(orderVM);
        }

        public IActionResult Result(OrderViewModel orderVM)
        {
            return View(orderVM);
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckDate(DateTime DateofDelivery)
        {
            TimeSpan diff = DateofDelivery.Subtract(DateTime.Now);
            if (diff.TotalDays > 0)
                return Json(true);
            return Json(false);
        }
    }
}