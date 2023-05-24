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

        public IActionResult Index(string sortOrder)
        {
            var productsViewModels = new List<ProductViewModel>();

            switch(sortOrder)
            {
                case "genre_landscape": 
                    productsViewModels = products.GetAll().Where(x => x.Genre == OnlineShop.Db.Models.Genre.Пейзаж).ToList().ToProductViewModels(); break;
                case "genre_stilllife":
                    productsViewModels = products.GetAll().Where(x => x.Genre == OnlineShop.Db.Models.Genre.Натюрморт).ToList().ToProductViewModels(); break;
                case "genre_animalism":
                    productsViewModels = products.GetAll().Where(x => x.Genre == OnlineShop.Db.Models.Genre.Анималистика).ToList().ToProductViewModels(); break;
                case "genre_portrait":
                    productsViewModels = products.GetAll().Where(x => x.Genre == OnlineShop.Db.Models.Genre.Портрет).ToList().ToProductViewModels(); break;
                case "cost_desc":
                    productsViewModels = products.GetAll().OrderByDescending(x => x.Cost).ToList().ToProductViewModels(); break;
                case "cost_asc":
                    productsViewModels = products.GetAll().OrderBy(x => x.Cost).ToList().ToProductViewModels(); break;
                case "promo":
                    productsViewModels = products.GetAll().Where(x => x.IsPromo == true).ToList().ToProductViewModels(); break;
                case "newProducts":
                    productsViewModels = products.GetAll().OrderByDescending(x => x.Id).ToList().ToProductViewModels(); break;
                default:
                    productsViewModels = products.GetAll().ToProductViewModels(); break;
            }
                return View(productsViewModels);
            }
        }
    }