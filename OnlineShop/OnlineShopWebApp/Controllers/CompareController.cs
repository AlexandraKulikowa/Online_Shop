using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Helpers;
using OnlineShop.Db.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using OnlineShop.Db;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CompareController : Controller
    {
        IUnitOfWork unitOfWork;
        public CompareController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var products = await unitOfWork.CompareDbRepository.GetAllAsync(Constants.UserId);
            var listVM = products.ToProductViewModels();
            return View(listVM);
        }
        public async Task<IActionResult> AddAsync(int id)
        {
            var product = await unitOfWork.ProductsDbRepository.TryGetByIdAsync(id);
            await unitOfWork.CompareDbRepository.AddAsync(product, Constants.UserId);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            await unitOfWork.CompareDbRepository.DeleteProductAsync(Constants.UserId, id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ClearAsync()
        {
            await unitOfWork.CompareDbRepository.ClearAsync(Constants.UserId);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}