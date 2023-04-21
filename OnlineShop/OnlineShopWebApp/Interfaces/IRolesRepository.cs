using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp.Interfaces
{
    public interface IRolesRepository
    {
        List<Role> GetAll();
        Role TryGetById(int id);
        void Add(Role role);
        void Delete(Role role);
    }
}
