using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository products;
        public HomeController(IProductsRepository products)
        {
            this.products = products;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            var productsDb = await products.GetAllAsync();
            var result = new List<Product>();

            switch(sortOrder)
            {
                case "genre_landscape":
                    result = await products.GetByGenreAsync(Genre.Пейзаж);
                    break;
                case "genre_still_life":
                    result = await products.GetByGenreAsync(Genre.Натюрморт);
                    break;
                case "genre_animalism":
                    result = await products.GetByGenreAsync(Genre.Анималистика);
                    break;
                case "genre_portrait":
                    result = await products.GetByGenreAsync(Genre.Портрет);
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