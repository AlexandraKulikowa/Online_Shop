using System.Collections.Generic;
using OnlineShopWebApp.Models;
using System.Linq;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Repositories
{
    public class InMemoryUsersRepository : IUsersRepository
    {
        private List<User> users = new List<User>();
        public List<User> GetAll()
        {
            return users;
        }
        public User TryGetById(int id)
        {
            return users.FirstOrDefault(user => user.Id == id);
        }

        public void Add(User checkUser)
        {
            var user = TryGetById(checkUser.Id);
            if (user == null)
            {
                users.Add(checkUser);
            }
        }

        public void Delete(int id)
        {
            var user = TryGetById(id);
            if (user != null)
            {
                users.Remove(user);
            }
        }

        public bool CheckUser(string login, string password)
        {
            var checkLogin = users.FirstOrDefault(x => x.Login ==login);
            var checkPassword = users.FirstOrDefault(x => x.Password == password);
            if (checkLogin == null || checkPassword == null)
                return true;
            return false;
        }

        public void ChangePassword(int id, string password)
        {
            var user = TryGetById(id);

           user.Name = product.Name;
            existingProduct.Cost = product.Cost;
        }
    }
}