using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class ProductList
    {
        private static List<Product> listProducts { get; set; }

        public ProductList()
        {
            listProducts = MakeFullProductList();
        }
        public static List<Product> MakeFullProductList()
        {
            listProducts = new List<Product>()
            {
                new Product ("Картина Золотая осень", 8000, "Осенний пейзаж",GenreEnum.Landscape, "масло", new Size (50, 60, true),2022,false),
                new Product ("Картина Пеннивайз", 6000, "Клоун из Оно",GenreEnum.Portrait, "масло",new Size (25, 30, false),2022,false),
                new Product ("Картина Бокал вина", 3000, "Картина для оформления интерьера кухни",GenreEnum.StillLife, "масло",new Size (20, 25, true),2022,true),
                new Product ("Картина Динозавр", 5000, "Тиранозавр Рекс",GenreEnum.Animalism, "масло", new Size(30, 35, false),2022,true),
                new Product ("Картина Лара Крофт", 2000, "Анджелина Джоли в роли Лары Крофт",GenreEnum.Portrait, "масло",new Size(20, 25, false),2021,true),
                new Product ("Картина Девушка и ветер", 4000, "Картина в подарок подруге", GenreEnum.Portrait, "масло", new Size(20, 25, false),2021,false),
            };

            return listProducts;
        }
        public List<Product> GetProducts()
        {
            return listProducts;
        }
    }
}
