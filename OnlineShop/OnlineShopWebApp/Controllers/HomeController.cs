using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository products;
        public HomeController(IProductsRepository products)
        {
            this.products = products;
        }
        public IActionResult Index(Genre? genre, string? sortOrder, bool? promo, bool? newProducts)
        {
            var productsViewModels = new List<ProductViewModel>();

            if (genre != null)
            {
                var result = products.GetAll().Where(x => x.Genre == (OnlineShop.Db.Models.Genre)genre).ToList();

                if (sortOrder == "cost_desc")
                    result = result.OrderByDescending(x => x.Cost).ToList();

                if (sortOrder == "cost_asc")
                    result = result.OrderBy(x => x.Cost).ToList();

                productsViewModels = result.ToProductViewModels();
                ViewBag.Genre = genre;
                return View(productsViewModels);
            }

            if (sortOrder != null)
            {
                var result = products.GetAll();

                if (sortOrder == "cost_desc")
                    result = result.OrderByDescending(x => x.Cost).ToList();

                if (sortOrder == "cost_asc")
                    result = result.OrderBy(x => x.Cost).ToList();

                if (genre != null)
                {
                    result = result.Where(x => x.Genre == (OnlineShop.Db.Models.Genre)genre).ToList();
                }

                productsViewModels = result.ToProductViewModels();
                ViewBag.SortOrder = sortOrder;
                return View(productsViewModels);
            }

            if (promo != null)
            {
                var result = products.GetAll().Where(x => x.IsPromo == true).ToList().ToProductViewModels();
                return View(result);
            }

            if (newProducts != null)
            {
                if (newProducts == true)
                {
                    var result = products.GetAll().OrderByDescending(x => x.Id).ToList().ToProductViewModels();
                    return View(result);
                }

                if (newProducts == false)
                {
                    var result = products.GetAll().OrderBy(x => x.Id).ToList().ToProductViewModels();
                    return View(result);
                }
            }

            var productsDb = products.GetAll();
            productsViewModels = productsDb.ToProductViewModels();

            return View(productsViewModels);
        }
    }
}