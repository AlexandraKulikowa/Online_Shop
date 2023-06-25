using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        IUnitOfWork unitOfWork;

        public BasketController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var basket = await unitOfWork.BasketsDbRepository.TryGetByUserIdAsync(Constants.UserId);
            var basketVM = basket.ToBasketViewModel();
            return View(basketVM);
        }
        public async Task<IActionResult> AddAsync(int id)
        {
            var product = await unitOfWork.ProductsDbRepository.TryGetByIdAsync(id);
            await unitOfWork.BasketsDbRepository.AddAsync(product, Constants.UserId);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangeAmount(int id, bool sign)
        {
            await unitOfWork.BasketsDbRepository.ChangeAmountAsync(id, sign, Constants.UserId);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Clear()
        {
            await unitOfWork.BasketsDbRepository.ClearAsync(Constants.UserId);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}