using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Helpers;
using OnlineShop.Db.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using OnlineShop.Db;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class FavouriteController : Controller
    {
        IUnitOfWork unitOfWork;
        public FavouriteController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var list = await unitOfWork.FavouriteDbRepository.GetAllAsync(Constants.UserId);
            var listVM = list.ToProductViewModels();
            return View(listVM);
        }

        public async Task<IActionResult> AddAsync(int id)
        {
            var product = await unitOfWork.ProductsDbRepository.TryGetByIdAsync(id);
            await unitOfWork.FavouriteDbRepository.AddAsync(product, Constants.UserId);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await unitOfWork.FavouriteDbRepository.DeleteFavouriteAsync(Constants.UserId, id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ClearAsync()
        {
            await unitOfWork.FavouriteDbRepository.ClearAsync(Constants.UserId);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}