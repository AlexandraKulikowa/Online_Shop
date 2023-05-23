using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System.Collections.Generic;
using System.Linq;
using Genre = OnlineShop.Db.Models.Genre;

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
                    result = productsDb
                        .Where(x => x.Genre == Genre.Пейзаж).ToList();
                    break;
                case "genre_still_life":
                    result = productsDb
                        .Where(x => x.Genre == Genre.Натюрморт).ToList();
                    break;
                case "genre_animalism":
                    result = productsDb.Where(x => x.Genre == Genre.Анималистика).ToList();
                    break;
                case "genre_portrait":
                    result = productsDb.Where(x => x.Genre == Genre.Портрет).ToList();
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
                return View(productsDb.ToProductViewModels());
            }
        }
    }