using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories;

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


        [HttpPost]
        public IActionResult CheckDate(string value)
        {
            var date = Convert.ToDateTime(value);
            TimeSpan diff = date.Subtract(DateTime.Now);
            if(diff.TotalDays > 0)
                return Json(true);
            return Json(false);
        }
    }
}
