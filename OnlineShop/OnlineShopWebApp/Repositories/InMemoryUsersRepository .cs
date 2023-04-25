using System.Collections.Generic;
using OnlineShopWebApp.Models;
using System.Linq;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Repositories
{
    public class InMemoryUsersRepository : IUsersRepository
    {
        private List<User> users = new List<User>();
        public List<User> Users => users;
        public List<User> GetAll()
        {
            return users;
        }
        public User TryGetById(int id)
        {
            if(users.Any())
            {
                return users.FirstOrDefault(user => user.Id == id); 
            }
            return null;
        }

        public void Add(User user)
        {
            var existingUser = TryGetById(user.Id);
            if (existingUser == null)
            {
                users.Add(user);
            }
        }

        public void Delete(int id)
        {
            var existingUser = TryGetById(id);
            if (existingUser != null)
            {
                users.Remove(existingUser);
            }
        }

        public bool CheckUser(string login)
        {
            var check = users.FirstOrDefault(x => x.Login ==login);
            if(check == null)
                return true;
            return false;
        }
    }
}