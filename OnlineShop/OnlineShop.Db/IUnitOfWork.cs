using OnlineShop.Db.Interfaces;
using System;

namespace OnlineShop.Db
{
    public interface IUnitOfWork : IDisposable
    {
        IProductsRepository ProductsDbRepository { get; }
        IBasketsRepository BasketsDbRepository { get; }
        IOrderRepository OrderDbRepository { get; }
        ICompareRepository CompareDbRepository { get; }
        IFavouriteRepository FavouriteDbRepository { get; }
        void Save();
    }
}
