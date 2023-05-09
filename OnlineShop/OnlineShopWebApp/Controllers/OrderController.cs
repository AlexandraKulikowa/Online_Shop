﻿using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Interfaces;
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
        public IActionResult ToCheckOut(Order order)
        {
            if (ModelState.IsValid)
            {
                var basket = baskets.TryGetByUserId(Constants.UserId);
                var basketVM = Mapping.ToBasketViewModel(basket);
                order.Products.AddRange(basketVM.BasketItems.ToArray());
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
            if (diff.TotalDays > 0)
                return Json(true);
            return Json(false);
        }
    }
}