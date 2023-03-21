using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShopWebApp.Models;
using Newtonsoft.Json;

namespace OnlineShopWebApp.Controllers
{    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private List<Product> ProductList()
        {
            var Products = new List<Product>()
            {
                new Product ("Картина Золотая осень", 8000, "Осенний пейзаж","Пейзаж", "масло","50*60",2022,false),
                new Product ("Картина Пеннивайз", 6000, "Клоун из Оно","Портрет", "масло","25*30",2022,false),
                new Product ("Картина Бокал вина", 3000, "Картина для оформления интерьера кухни","Натюрморт", "масло","20*25",2022,true),
                new Product ("Картина Динозавр", 5000, "Тиранозавр Рекс","Пейзаж", "масло","30*35",2022,true),
                new Product ("Картина Лара Крофт", 2000, "Анджелина Джоли в роли Лары Крофт","Портрет", "масло","20*25",2021,true),
                new Product ("Картина Девушка и ветер", 4000, "Картина в подарок подруге","Портрет", "масло","20*25",2021,false),
            };

            var jsonProducts = JsonConvert.SerializeObject(Products);
            System.IO.File.WriteAllText(@"jsonproducts.json", jsonProducts);
            var ListOfProducts = JsonConvert.DeserializeObject<List<Product>>(jsonProducts);
            return Products;
        }
        public string Index()
        {
            var Products = ProductList();
            var result = String.Join("", Products);
            return result;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
