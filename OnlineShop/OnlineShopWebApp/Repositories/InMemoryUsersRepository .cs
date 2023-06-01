using System.Collections.Generic;
using System.Linq;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public class InMemoryUsersRepository : IUsersRepository
    {
        private List<UserViewModel> users = new List<UserViewModel>();
        public List<UserViewModel> GetAll()
        {
            return users;
        }
        public UserViewModel TryGetById(int id)
        {
            return users.FirstOrDefault(user => user.Id == id);
        }

        public void Add(UserViewModel checkUser)
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
            var check = users.FirstOrDefault(x => x.Login == login && x.Password == password);
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

        public void ChangeUser(UserViewModel user)
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