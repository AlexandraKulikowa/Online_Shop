using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShop.Db.Repositories;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IBasketsRepository baskets;
        private readonly IOrderRepository orders;
        public OrderController(IBasketsRepository baskets, IOrderRepository orders)
        {
            this.baskets = baskets;
            this.orders = orders;
        }
        public async Task<IActionResult> Index()
        {
            var basket = await baskets.TryGetByUserIdAsync(Constants.UserId);
            var basketVM = basket.ToBasketViewModel();
            ViewBag.Basket = basketVM.BasketItems.Any();
            ViewBag.TotalCost = basketVM.TotalCost();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ToCheckOutAsync(OrderViewModel orderVM)
        {
            if (ModelState.IsValid)
            {
                var basket = await baskets.TryGetByUserIdAsync(Constants.UserId);
                var order = orderVM.ToOrder();
                order.OrderBasketItems.AddRange(basket.BasketItems.ToArray());
                await orders.AddAsync(order);
                await baskets.ClearAsync(Constants.UserId);
                return RedirectToAction("Result");
            }
            return View(orderVM);
        }

        public async Task<IActionResult> Result()
        {
            ViewBag.Id = (await orders.GetAllAsync()).Count;
            return View();
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