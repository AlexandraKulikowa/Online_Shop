using System.Collections.Generic;
using OnlineShopWebApp.Models;
using System.Linq;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Repositories
{
    public class InMemoryRolesRepository : IRolesRepository
    {
        private List<Role> roles;
        public InMemoryRolesRepository()
        {
            roles = new List<Role>()
            {
                new Role ("Админ", "Может всё"),
                new Role ("Пользователь", "Может сделать заказ, сравнивать товары, пользоваться личным кабинетом"),
                new Role ("Модератор", "Может добавлять, изменять и удалять товары"),
                new Role ("Гость", "Не может сделать заказ")
                };
        }
        public List<Role> GetAll()
        {
            return roles;
        }
        public Role TryGetById(int id)
        {
            return roles.FirstOrDefault(role => role.Id == id);
        }

        public void Add(Role role)
        {
            var existingRole = TryGetById(role.Id);
            if (existingRole == null)
            {
                roles.Add(role);
            }
        }

        public void Delete(int id)
        {
            var existingRole = TryGetById(id);
            if (existingRole != null)
            {
                roles.Remove(existingRole);
            }
        }

        public bool CheckRole(Role role)
        {
            var roleslist = GetAll();


            foreach (var item in roleslist)
            {
                if (item.Name == role.Name)
                {
                    return false;
                }
            }
            return true;
        }
    }
}