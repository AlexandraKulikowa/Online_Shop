﻿using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class ProductRepository
    {
        private static List<Product> listProducts = new List<Product>()
            {
                new Product ("Картина \"Золотая осень\"", 8000, "Осенний пейзаж",GenreEnum.Пейзаж, "масло", new Size (50, 60, true),2022,false),
                new Product ("Картина \"Пеннивайз\"", 6000, "Клоун из Оно",GenreEnum.Портрет, "масло",new Size (25, 30, false),2022,false),
                new Product ("Картина \"Бокал вина\"", 3000, "Картина для оформления интерьера кухни",GenreEnum.Натюрморт, "масло",new Size (20, 25, true),2022,true),
                new Product ("Картина \"Динозавр\"", 5000, "Тиранозавр Рекс",GenreEnum.Анималистика, "масло", new Size(30, 35, false),2022,true),
                new Product ("Картина \"Лара Крофт\"", 2000, "Анджелина Джоли в роли Лары Крофт",GenreEnum.Портрет, "масло",new Size(20, 25, false),2021,true),
                new Product ("Картина \"Девушка и ветер\"", 4000, "Картина в подарок подруге", GenreEnum.Портрет, "масло", new Size(20, 25, false),2021,false),
            };

        public List<Product> GetAll()
        {
            return listProducts;
        }
        public Product TryGetById(int id)
        {
            return listProducts.FirstOrDefault(product  => product.Id == id);
        }
    }
}
