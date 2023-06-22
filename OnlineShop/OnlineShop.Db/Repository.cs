using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> entities;

        public Repository(DatabaseContext databaseContext)
        {
            entities = databaseContext.Set<T>();
        }

        public async Task<T> TryGetByIdAsync(object id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await entities.AddAsync(entity);
        }
        public async Task DeleteAsync(T entity)
        {
            entities.Remove(entity);
        }
    }
}
}
