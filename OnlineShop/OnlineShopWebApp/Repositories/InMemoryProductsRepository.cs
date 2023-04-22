using System.Collections.Generic;
using System.Linq;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public class InMemoryProductsRepository : IProductsRepository
    {
        private List<Product> listProducts;
        public InMemoryProductsRepository()
        {
            listProducts = new List<Product>()
            {
                new Product ("Картина \"Золотая осень\"", 8000, "Осенний пейзаж",Genre.Пейзаж, "масло", new Size (50, 60, true),2022,false, "/images/AutumnBig.jpg"),
                new Product ("Картина \"Пеннивайз\"", 6000, "Клоун из Оно",Genre.Портрет, "масло",new Size (25, 30, false),2022,false, "/images/PennywiseBig.jpg"),
                new Product ("Картина \"Бокал вина\"", 3000, "Картина для интерьера",Genre.Натюрморт, "масло",new Size (20, 25, true),2022,true, "/images/WineBig.jpg"),
                new Product ("Картина \"Динозавр\"", 5000, "Тиранозавр Рекс",Genre.Анималистика, "масло", new Size(30, 35, false),2022,true, "/images/DinoBig.jpg"),
                new Product ("Картина \"Лара Крофт\"", 2000, "Анджелина Джоли в роли Лары Крофт",Genre.Портрет, "масло",new Size(20, 25, false),2021,true, "/images/LaraCroftBig.jpg"),
                new Product ("Картина \"Девушка и ветер\"", 4000, "Картина в подарок подруге", Genre.Портрет, "масло", new Size(20, 25, false),2021,false, "/images/GirlAndWindBig.jpg"),
            };
        }

        public List<Product> GetAll()
        {
            return listProducts;
        }
        public Product TryGetById(int id)
        {
            return listProducts.FirstOrDefault(product => product.Id == id);
        }

        public List<Product> Search(string name)
        {
            name = name.ToLower();
            var result = listProducts.Where(x => x.Name.ToLower().Contains(name)).ToList();
            return result;
        }

        public void Add(Product product)
        {
            var existingProduct = TryGetById(product.Id);
            if (existingProduct == null)
            {
                listProducts.Add(product);
            }
        }

        public void Delete(Product product)
        {
            var existingProduct = TryGetById(product.Id);
            if (existingProduct != null)
            {
                listProducts.Remove(product);
            }
        }

        public void Edit(Product product)
        {
            var existingProduct = TryGetById(product.Id);

            existingProduct.Name = product.Name;
            existingProduct.Cost = product.Cost;
            existingProduct.Description = product.Description;
            existingProduct.Genre = product.Genre;
            existingProduct.PaintingTechnique = product.PaintingTechnique;
            existingProduct.Size = product.Size;
            existingProduct.Year = product.Year;
            existingProduct.IsPromo = product.IsPromo;
            existingProduct.ImagePath = product.ImagePath;
        }

        public bool CheckNewProduct(Product product)
        {
            if (product.Name == product.Description)
                return false;
            return true;
        }
    }
}