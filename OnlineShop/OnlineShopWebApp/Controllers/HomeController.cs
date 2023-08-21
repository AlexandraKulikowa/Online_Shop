
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
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
        IUnitOfWork unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            IList<Product> productsDb = await unitOfWork.ProductsDbRepository.GetAllAsync();
            var result = new List<Product>();

            switch(sortOrder)
            {
                case "genre_landscape":
                    result = await unitOfWork.ProductsDbRepository.GetByGenreAsync(Genre.Натюрморт);
                    break;
                case "genre_still_life":
                    result = await unitOfWork.ProductsDbRepository.GetByGenreAsync(Genre.Пейзаж);
                    break;
                case "genre_animalism":
                    result = await unitOfWork.ProductsDbRepository.GetByGenreAsync(Genre.Анималистика);
                    break;
                case "genre_portrait":
                    result = await unitOfWork.ProductsDbRepository.GetByGenreAsync(Genre.Портрет);
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
                    result = (List<Product>)productsDb;
                    break;
            }
                return View(result.ToProductViewModels());
            }
        }
    }