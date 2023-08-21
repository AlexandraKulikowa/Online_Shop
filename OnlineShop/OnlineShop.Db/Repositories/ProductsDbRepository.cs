using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Product>> GetAllAsync()
        {
            return await databaseContext.Products
                .Include(x => x.Size)
                .Include(x => x.ImagePath)
                .ToListAsync();
        }

        public async Task<Product> TryGetByIdAsync(int id)
        {
            return await databaseContext.Products
                .Include(x => x.Size)
                .Include(x => x.ImagePath)
                .FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task<List<Product>> SearchAsync(string name)
        {
            if (name == null)
                return null;
            name = name.ToLower();
            var result = await databaseContext.Products
                .Include(x => x.Size)
                .Include(x => x.ImagePath)
                .Where(x => x.Name
                .ToLower()
                .Contains(name))
                .ToListAsync();
            return result;
        }

        public async Task AddAsync(Product product)
        {
            var existingProduct = await TryGetByIdAsync(product.Id);
            if (existingProduct == null)
            {
                await databaseContext.Products.AddAsync(product);
            }
        }

        public async Task DeleteAsync(Product product)
        {
            var existingProduct = await TryGetByIdAsync(product.Id);
            if (existingProduct != null)
            {
                databaseContext.Products.Remove(product);
            }
        }

        public async Task EditAsync(Product product)
        {
            var existingProduct = await TryGetByIdAsync(product.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Cost = product.Cost;
                existingProduct.Description = product.Description;
                existingProduct.Genre = product.Genre;
                existingProduct.PaintingTechnique = product.PaintingTechnique;
                existingProduct.Size = product.Size;
                existingProduct.SizeId = product.SizeId;
                existingProduct.Year = product.Year;
                existingProduct.IsPromo = product.IsPromo;
                existingProduct.ImagePath = product.ImagePath;
                existingProduct.BasketItems = product.BasketItems;
                existingProduct.ImagePath = product.ImagePath;
            }
        }

        public bool CheckNewProduct(Product product)
        {
            if (product.Name == product.Description)
                return false;
            return true;
        }

        public async Task <List<Product>> GetByGenreAsync(Genre genre)
        {
            var result = await databaseContext.Products
                .Include(x => x.Size)
                .Include(x => x.ImagePath)
                .Where(x => x.Genre == genre)
                .ToListAsync();
            return result;
        }
    }
}