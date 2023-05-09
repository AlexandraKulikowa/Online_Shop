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
            existingProduct.BasketItems = product.BasketItems;

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