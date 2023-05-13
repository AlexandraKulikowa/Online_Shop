using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly IOrderRepository orders;
        public OrderDetailsController(IOrderRepository orders)
        {
            this.orders = orders;
        }
        public IActionResult Index(int id)
        {
            var order = orders.GetOrder(id);
            var orderVM = order.ToOrderViewModel();
            return View(orderVM);
        }
    }
}