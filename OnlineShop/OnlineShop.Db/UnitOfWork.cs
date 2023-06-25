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
                return new ProductsDbRepository(databaseContext);
            }
        }

        public IBasketsRepository BasketsDbRepository
        {
            get
            {
                return new BasketsDbRepository(databaseContext);
            }
        }

        public IOrderRepository OrderDbRepository
        {
            get
            {
                return new OrdersDbRepository(databaseContext);
            }
        }

        public ICompareRepository CompareDbRepository
        {
            get
            {
                return new ComparesDbRepository(databaseContext);
            }
        }
        public IFavouriteRepository FavouriteDbRepository
        {
            get
            {
                return new FavouritesDbRepository(databaseContext);
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
