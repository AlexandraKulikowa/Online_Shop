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
        private IRepository<Product> productsRepo;
        private DatabaseContext databaseContext;
        private bool disposed = false;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IProductsRepository ProductsDbRepository
        {
            get
            {
                return (IProductsRepository)productsRepo;
            }
        }

        public void Save()
        {
            this.databaseContext.SaveChanges();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            return new Repository<T>(databaseContext);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    da.Dispose();
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
