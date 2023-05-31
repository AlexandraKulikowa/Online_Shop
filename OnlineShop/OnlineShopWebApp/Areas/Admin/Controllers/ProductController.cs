using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.IO;
using System;
using System.Linq;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductsRepository products;
        private readonly IWebHostEnvironment appEnvironment;
        public ProductController(IProductsRepository products, IWebHostEnvironment appEnvironment)
        {
            this.products = products;
            this.appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            var productlist = products.GetAll();
            var productsVM = productlist.ToProductViewModels();
            return View(productsVM);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productVM)
        {
            if (productVM.UploadedFiles != null)
            {
                string productImagesPath = Path.Combine(appEnvironment.WebRootPath + "/images/products");
                if (!Directory.Exists(productImagesPath))
                {
                    Directory.CreateDirectory(productImagesPath);
                }

                foreach (var file in productVM.UploadedFiles)
                {
                    var fileName = Guid.NewGuid() + "." + file.FileName.Split('.').Last();
                    using (var fileStream = new FileStream(productImagesPath + fileName, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.ImagePath.Add("/images/products" + fileName);
                }
            }

            var product = productVM.ToProduct();

            if (!products.CheckNewProduct(product))
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");

            if (ModelState.IsValid)
            {
                products.Add(product);
                return RedirectToAction("Index");
            }
            return View("Add", productVM);
        }

        public IActionResult Edit(int id)
        {
            var product = products.TryGetById(id);
            var productVM = product.ToProductViewModel();
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productVM)
        {
            if (productVM.UploadedFiles != null)
            {
                string productImagesPath = Path.Combine(appEnvironment.WebRootPath + "/images/products");
                if (!Directory.Exists(productImagesPath))
                {
                    Directory.CreateDirectory(productImagesPath);
                }

                foreach (var file in productVM.UploadedFiles)
                {
                    var fileName = Guid.NewGuid() + "." + file.FileName.Split('.').Last();
                    using (var fileStream = new FileStream(productImagesPath + fileName, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.ImagePath.Add("/images/products" + fileName);
                }
            }

            var product = productVM.ToProduct();

            if (!products.CheckNewProduct(product))
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");

            if (ModelState.IsValid)
            {
                products.Edit(product);
                return RedirectToAction("Index");
            }

            return View("Edit", productVM);
        }

        public IActionResult Delete(int id)
        {
            var product = products.TryGetById(id);
            products.Delete(product);
            return RedirectToAction("Index");
        }
    }
}