﻿using System.Collections.Generic;
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
            return favouriteList.FirstOrDefault(x => x.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var existingList = TryGetByUserId(userId);
            if (existingList == null)
            {
                var newList = new Favourites
                {
                    UserId = userId,
                    Products = new List<Product> { product }
                };
                favouriteList.Add(newList);
            }
            else
            {
                var existingProduct = existingList.Products.FirstOrDefault(x => x.Id == product.Id);
                if (existingProduct == null)
                {
                    existingList.Products.Add(product);
                }
            }
        }

        public void DeleteFavourite(string userId, int id)
        {
            var list = TryGetByUserId(userId);
            var product = list.Products.FirstOrDefault(x => x.Id == id);
            list.Products.Remove(product);
        }
        public void Clear(string userId)
        {
            var list = TryGetByUserId(userId);
            list.Products.Clear();
        }
    }
}