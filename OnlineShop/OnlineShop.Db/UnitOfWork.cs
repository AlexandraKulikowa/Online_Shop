using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Repositories;
using System;

namespace OnlineShop.Db
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext databaseContext;
        private IProductsRepository productsDbRepository;
        private IBasketsRepository basketsDbRepository;
        private IOrderRepository orderDbRepository;
        private ICompareRepository compareDbRepository;
        private IFavouriteRepository favouriteDbRepository;
        private bool disposed = false;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IProductsRepository ProductsDbRepository
        {
            get
            {
                if(productsDbRepository == null)
                {
                    return new ProductsDbRepository(databaseContext);
                }
                return productsDbRepository;
            }
        }

        public IBasketsRepository BasketsDbRepository
        {
            get
            {
                if(basketsDbRepository == null)
                {
                    return new BasketsDbRepository(databaseContext);
                }
                return basketsDbRepository;
            }
        }

        public IOrderRepository OrderDbRepository
        {
            get
            {
                if(orderDbRepository == null)
                {
                    return new OrdersDbRepository(databaseContext);
                }
                return orderDbRepository;
            }
        }

        public ICompareRepository CompareDbRepository
        {
            get
            {
                if(compareDbRepository == null)
                {
                    return new ComparesDbRepository(databaseContext);
                }
                return compareDbRepository;
            }
        }
        public IFavouriteRepository FavouriteDbRepository
        {
            get
            {
                if(favouriteDbRepository == null)
                {
                    return new FavouritesDbRepository(databaseContext);
                }
                return favouriteDbRepository;
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