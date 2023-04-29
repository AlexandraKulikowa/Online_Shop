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
            var check = users.FirstOrDefault(x => x.Login ==login && x.Password == password);
            if (check == null)
                return true;
            return false;
        }

        public bool arePasswordsEqual(int id, string password)
        {
            var user = TryGetById(id);
            if(user.Password == password)
                return true;
            return false;
        }

        public void ChangePassword(int id, string password, string confirmpassword)
        {
            var user = TryGetById(id);
            user.Password = password;
            user.ConfirmPassword = confirmpassword;
        }

        public void ChangeUser(User user)
        {
            var currentUser = TryGetById(user.Id);

            currentUser.Surname = user.Surname;
            currentUser.Name = user.Name;
            currentUser.Fathername = user.Fathername;
            currentUser.Login = user.Login;
            currentUser.Email = user.Email;
            currentUser.Phone = user.Phone;
            currentUser.isDistribution = user.isDistribution;
        }
    }
}