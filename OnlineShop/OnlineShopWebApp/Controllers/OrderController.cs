using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShop.Db.Repositories;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using OnlineShop.Db;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var basket = await unitOfWork.BasketsDbRepository.TryGetByUserIdAsync(Constants.UserId);
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
                var basket = await unitOfWork.BasketsDbRepository.TryGetByUserIdAsync(Constants.UserId);
                var order = orderVM.ToOrder();
                order.OrderBasketItems.AddRange(basket.BasketItems.ToArray());
                await unitOfWork.OrderDbRepository.AddAsync(order);
                await unitOfWork.BasketsDbRepository.ClearAsync(Constants.UserId);
                unitOfWork.Save();
                return RedirectToAction("Result");
            }
            return View(orderVM);
        }

        public async Task<IActionResult> Result()
        {
            ViewBag.Id = (await unitOfWork.OrderDbRepository.GetAllAsync()).Count;
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