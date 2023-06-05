using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orders;
        public OrderController(IOrderRepository orders) => this.orders = orders;

        public async Task<IActionResult> Index()
        {
            var listOrders = await orders.GetAllAsync();
            var listOrdersVM = listOrders.ToOrderViewModels();
            return View(listOrdersVM);
        }

        public async Task<IActionResult> DetailsAsync(int id)
        {
            var order = await orders.GetOrderAsync(id);
            var orderVM = order.ToOrderViewModel();
            return View(orderVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditStatusAsync(int id, Status status)
        {
            await orders.ChangeStatusAsync(id, status);
            return RedirectToAction("Index");
        }
    }
}