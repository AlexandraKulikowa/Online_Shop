using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class OrderDetails : Controller
    {
        private readonly IOrderRepository orders;
        public OrderDetails(IOrderRepository orders)
        {
            this.orders = orders;
        }

        public IActionResult Index(int id)
        {
            var order = orders.GetOrder(id);
            return View(order);
        }
    }
}
