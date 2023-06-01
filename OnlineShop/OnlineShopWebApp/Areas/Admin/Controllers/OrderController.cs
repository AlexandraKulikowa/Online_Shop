﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orders;
        public OrderController(IOrderRepository orders) => this.orders = orders;

        public IActionResult Index()
        {
            var listOrders = orders.GetAll();
            var listOrdersVM = listOrders.ToOrderViewModels();
            return View(listOrdersVM);
        }

        public IActionResult Details(int id)
        {
            var order = orders.GetOrder(id);
            var orderVM = order.ToOrderViewModel();
            return View(orderVM);
        }

        [HttpPost]
        public IActionResult EditStatus(int id, Status status)
        {
            orders.ChangeStatus(id, status);
            return RedirectToAction("Index");
        }
    }
}