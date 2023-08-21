using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
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
        IUnitOfWork unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var listOrders = await unitOfWork.OrderDbRepository.GetAllAsync();
            var listOrdersVM = listOrders.ToOrderViewModels();
            return View(listOrdersVM);
        }

        public async Task<IActionResult> DetailsAsync(int id)
        {
            var order = await unitOfWork.OrderDbRepository.GetOrderAsync(id);
            var orderVM = order.ToOrderViewModel();
            return View(orderVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditStatusAsync(int id, Status status)
        {
            await unitOfWork.OrderDbRepository.ChangeStatusAsync(id, status);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}