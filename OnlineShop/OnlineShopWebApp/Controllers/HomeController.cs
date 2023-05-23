using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
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

        public IActionResult Index(string? sortOrder)
        {
            var productsDb = products.GetAll();
            var result = new List<Product>();

            switch(sortOrder)
            {
                case "genre_landscape":
                    result = products.GetByGenre(Genre.Натюрморт);
                    break;
                case "genre_still_life":
                    result = products.GetByGenre(Genre.Пейзаж);
                    break;
                case "genre_animalism":
                    result = products.GetByGenre(Genre.Анималистика);
                    break;
                case "genre_portrait":
                    result = products.GetByGenre(Genre.Портрет);
                    break;
                case "cost_desc":
                    result = productsDb.OrderByDescending(x => x.Cost).ToList();
                    break;
                case "cost_asc":
                    result = productsDb.OrderBy(x => x.Cost).ToList();
                    break;
                case "promo":
                    result = productsDb.Where(x => x.IsPromo == true).ToList();
                    break;
                case "newProducts":
                    result = productsDb.OrderByDescending(x => x.Id).ToList();
                    break;
                default:
                    result = productsDb;
                    break;
            }
                return View(result.ToProductViewModels());
            }
        }
    }