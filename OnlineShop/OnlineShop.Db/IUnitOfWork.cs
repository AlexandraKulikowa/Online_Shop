using OnlineShop.Db.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        IProductsRepository ProductsDbRepository { get; }

    }
}
