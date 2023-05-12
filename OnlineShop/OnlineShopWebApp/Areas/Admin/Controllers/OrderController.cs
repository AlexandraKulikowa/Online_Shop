using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;

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
            var listOrdersVM = Mapping.ToOrderViewModels(listOrders);
            return View(listOrdersVM);
        }

        public IActionResult Details(int id)
        {
            var order = orders.GetOrder(id);
            var orderVM = Mapping.ToOrderViewModel(order);
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