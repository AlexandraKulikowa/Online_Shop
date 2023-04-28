using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp.Interfaces
{
    public interface IUsersRepository
    {
        List<User> GetAll();
        User TryGetById(int id);
        void Add(User user);
        void Delete(int id);
        bool CheckUser(string login, string password);
        void ChangePassword(int id, string password, string confirmpassword);
        bool arePasswordsEqual(int id, string password);
        void ChangeUser(User user);
    }
}
