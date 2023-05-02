using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orders;
        public OrderController(IOrderRepository orders) => this.orders = orders;

        public IActionResult Index()
        {
            var listOrders = orders.GetAll();
            return View(listOrders);
        }

        public IActionResult Details(int id)
        {
            var order = orders.GetOrder(id);
            return View(order);
        }

        [HttpPost]
        public IActionResult EditStatus(int id, Status status)
        {
            orders.ChangeStatus(id, status);
            return RedirectToAction("Index");
        }
    }
}