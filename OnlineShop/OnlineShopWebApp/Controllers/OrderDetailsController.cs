using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderDetailsController : Controller
    {
        private readonly IOrderRepository orders;
        public OrderDetailsController(IOrderRepository orders)
        {
            this.orders = orders;
        }

        public async Task<IActionResult> Index(int id)
        {
            var order = await orders.GetOrderAsync(id);
            var orderVM = order.ToOrderViewModel();
            return View(orderVM);
        }
    }
}