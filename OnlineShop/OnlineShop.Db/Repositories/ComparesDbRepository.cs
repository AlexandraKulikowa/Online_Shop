using System.Collections.Generic;
using System.Linq;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class ComparesDbRepository : ICompareRepository
    {
        private readonly DatabaseContext databaseContext;

        public ComparesDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Comparison TryGetByUserId(string userId)
        {
            return databaseContext.Comparisons.FirstOrDefault(x => x.UserId == userId);
        }
        public void Add(Product product, string userId)
        {
            var existingList = TryGetByUserId(userId);

            if (existingList == null)
            {
                var newList = new Comparison
                {
                    UserId = userId
                };

                newList.Products = new List<Product>
                    {
                        new Product
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Cost = product.Cost,
                            Description = product.Description,
                            Size = product.Size,
                            PaintingTechnique = product.PaintingTechnique,
                            Genre = product.Genre,
                            Year = product.Year,
                            ImagePath = product.ImagePath,
                            IsPromo = product.IsPromo,
                            Comparison = newList
                        }
                    };

                databaseContext.Comparisons.Add(newList);
            }
            else
            {
                var existingProduct = existingList.Products.FirstOrDefault(x => x.Id == product.Id);
                if (existingProduct == null)
                {
                    existingList.Products.Add(new Product
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Cost = product.Cost,
                        Description = product.Description,
                        Size = product.Size,
                        PaintingTechnique = product.PaintingTechnique,
                        Genre = product.Genre,
                        Year = product.Year,
                        ImagePath = product.ImagePath,
                        IsPromo = product.IsPromo,
                        Comparison = existingList
                    });
                }
            }
            databaseContext.SaveChanges();
        }

        public void DeleteProduct(string userId, int id)
        {
            var list = TryGetByUserId(userId);
            var product = list.Products.FirstOrDefault(x => x.Id == id);
            list.Products.Remove(product);
            databaseContext.SaveChanges();
        }
        public void Clear(string userId)
        {
            var list = TryGetByUserId(userId);
            databaseContext.Comparisons.Remove(list);
            databaseContext.SaveChanges();
        }
    }
}