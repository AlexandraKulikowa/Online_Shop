using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        IUnitOfWork unitOfWork;
        private readonly CreateProductHelper createProductHelper;
        public ProductController(IUnitOfWork unitOfWork, CreateProductHelper createProductHelper)
        {
            this.unitOfWork = unitOfWork;
            this.createProductHelper = createProductHelper;
        }

        public async Task<IActionResult> Index()
        {
            var productlist = await unitOfWork.ProductsDbRepository.GetAllAsync();
            var productsVM = productlist.ToProductViewModels();
            return View(productsVM);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductViewModel productVM)
        {
            var product = createProductHelper.CreateProduct(productVM);

            if (!unitOfWork.ProductsDbRepository.CheckNewProduct(product))
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");

            if (ModelState.IsValid)
            {
                await unitOfWork.ProductsDbRepository.AddAsync(product);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View("Add", productVM);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            var product = await unitOfWork.ProductsDbRepository.TryGetByIdAsync(id);
            var productVM = product.ToProductViewModel();
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(ProductViewModel productVM)
        {
            var product = createProductHelper.CreateProduct(productVM);

            if (!unitOfWork.ProductsDbRepository.CheckNewProduct(product))
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");

            if (ModelState.IsValid)
            {
                await unitOfWork.ProductsDbRepository.EditAsync(product);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View("Edit", productVM);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product = await unitOfWork.ProductsDbRepository.TryGetByIdAsync(id);
            await unitOfWork.ProductsDbRepository.DeleteAsync(product);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

    }
}