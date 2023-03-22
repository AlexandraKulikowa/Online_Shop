using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class ProductList
    {
        public List<Product> Products { get; set; }
        public ProductList()
        {
            Products = MakeFullProductList();
        }
        public List<Product> MakeFullProductList()
        {
            var Products = new List<Product>()
            {
                new Product ("Картина Золотая осень", 8000, "Осенний пейзаж",Product.GenreEnum.Landscape, "масло", new Size (50, 60, true),2022,false),
                new Product ("Картина Пеннивайз", 6000, "Клоун из Оно",Product.GenreEnum.Portrait, "масло",new Size (25, 30, false),2022,false),
                new Product ("Картина Бокал вина", 3000, "Картина для оформления интерьера кухни",Product.GenreEnum.StillLife, "масло",new Size (20, 25, true),2022,true),
                new Product ("Картина Динозавр", 5000, "Тиранозавр Рекс",Product.GenreEnum.Animalism, "масло", new Size(30, 35, false),2022,true),
                new Product ("Картина Лара Крофт", 2000, "Анджелина Джоли в роли Лары Крофт",Product.GenreEnum.Portrait, "масло",new Size(20, 25, false),2021,true),
                new Product ("Картина Девушка и ветер", 4000, "Картина в подарок подруге",Product.GenreEnum.Portrait, "масло", new Size(20, 25, false),2021,false),
            };
            return Products;
        }
        public void ToJsonList()
        {
            var jsonProducts = JsonConvert.SerializeObject(Products);
            System.IO.File.WriteAllText(@"jsonproducts.json", jsonProducts);
        }
        public List<Product> ToList(JsonResult jsonResult)
        {
            var jsonListProducts = System.IO.File.ReadAllText(@"jsonproducts.json");
            var ListOfProducts = JsonConvert.DeserializeObject<List<Product>>(jsonListProducts);
            return ListOfProducts;
        }


    }
}
