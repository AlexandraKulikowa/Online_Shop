using System.Collections.Generic;
using System.Linq;
using OnlineShop.Db.Models;
using OnlineShop.Db.Interfaces;

namespace OnlineShop.Db.Repositories
{
    public class FavouritesDbRepository : IFavouriteRepository
    {
        private readonly DatabaseContext databaseContext;

        public FavouritesDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Favourites TryGetByUserId(string userId)
        {
            return databaseContext.Favorites.FirstOrDefault(x => x.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var existingList = TryGetByUserId(userId);

            if (existingList == null)
            {
                var newList = new Favourites
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
                            Favourites = newList
                        }
                    };

                databaseContext.Favorites.Add(newList);
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
                        Favourites = existingList
                    });
                }
            }
        }

        public void DeleteFavourite(string userId, int id)
        {
            var list = TryGetByUserId(userId);
            var product = list.Products.FirstOrDefault(x => x.Id == id);
            list.Products.Remove(product);
            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var list = TryGetByUserId(userId);
            databaseContext.Favorites.Remove(list);
            databaseContext.SaveChanges();
        }
    }
}