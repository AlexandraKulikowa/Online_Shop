using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class ProductsDbRepository : IProductsRepository
    {
        private readonly DatabaseContext databaseContext;

        public ProductsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

    //    public ProductsDbRepository()
    //    {
    //        listProducts = new List<Product>()
    //        {
    //            new Product ("Картина \"Золотая осень\"", 8000, "Осенний пейзаж",Genre.Пейзаж, "масло", new Size (50, 60, true),2022,false, "/images/AutumnBig.jpg"),
    //            new Product ("Картина \"Орлы\"", 8000, "Парная картина", Genre.Анималистика, "масло", new Size(20, 25, false),2022,false, "/images/Eagles.jpg"),
    //            new Product ("Картина \"Пеннивайз\"", 6000, "Клоун из Оно",Genre.Портрет, "масло",new Size (25, 30, false),2022,false, "/images/PennywiseBig.jpg"),
    //            new Product ("Картина \"Лара Крофт\"", 2000, "Анджелина Джоли в роли Лары Крофт",Genre.Портрет, "масло",new Size(20, 25, false),2021,true, "/images/LaraCroftBig.jpg"),
    //            new Product ("Картина \"Бокал вина\"", 3000, "Картина для интерьера",Genre.Натюрморт, "масло",new Size (20, 25, true),2022,true, "/images/WineBig.jpg"),
    //            new Product ("Картина \"Девушка и ветер\"", 4000, "Картина в подарок подруге", Genre.Портрет, "масло", new Size(20, 25, false),2021,false, "/images/GirlAndWindBig.jpg"),
    //            new Product ("Картина \"Динозавр\"", 5000, "Тиранозавр Рекс",Genre.Анималистика, "масло", new Size(30, 35, false),2022,true, "/images/DinoBig.jpg"),
    //            new Product ("Картина \"Депрессия\"", 5500, "Картина в подарок подростку", Genre.Портрет, "масло", new Size(20, 15, false),2022,false, "/images/Depression.jpg"),

    //};
    //    }

        public List<Product> GetAll()
        {
            return databaseContext.Products.Include(x => x.Size).ToList();
        }
        public Product TryGetById(int id)
        {
            return databaseContext.Products.Include(x => x.Size).FirstOrDefault(product => product.Id == id);
        }

        public List<Product> Search(string name)
        {
            if (name == null)
                return null;
            name = name.ToLower();
            var result = databaseContext.Products.Include(x => x.Size).Where(x => x.Name.ToLower().Contains(name)).ToList();
            return result;
        }

        public void Add(Product product)
        {
            var existingProduct = TryGetById(product.Id);
            if (existingProduct == null)
            {
                databaseContext.Products.Add(product);
                databaseContext.SaveChanges();
            }
        }

        public void Delete(Product product)
        {
            var existingProduct = TryGetById(product.Id);
            if (existingProduct != null)
            {
                databaseContext.Products.Remove(product);
                databaseContext.SaveChanges();
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

            databaseContext.SaveChanges();
        }

        public bool CheckNewProduct(Product product)
        {
            if (product.Name == product.Description)
                return false;
            return true;
        }
    }
}