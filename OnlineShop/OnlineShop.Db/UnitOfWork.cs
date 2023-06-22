using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public class UnitOfWork : IUnitOfWork
    {
        //private IRepository<Product> productsRepo;
        private DatabaseContext databaseContext;
        private IProductsRepository productsDbRepository;
        private bool disposed = false;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IProductsRepository ProductsDbRepository
        {
            get
            {
                return productsDbRepository = productsDbRepository ?? new ProductsDbRepository(databaseContext);
            }
        }

        public void Save()
        {
            this.databaseContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    databaseContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
