using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public interface IRepository<T> where T : class
    {
        Task<T> TryGetByIdAsync(object id);
        Task<IList<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
