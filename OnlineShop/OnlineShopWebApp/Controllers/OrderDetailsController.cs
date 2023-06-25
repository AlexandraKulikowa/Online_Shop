using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderDetailsController : Controller
    {
        IUnitOfWork unitOfWork;
        public OrderDetailsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int id)
        {
            var order = await unitOfWork.OrderDbRepository.GetOrderAsync(id);
            var orderVM = order.ToOrderViewModel();
            return View(orderVM);
        }
    }
}