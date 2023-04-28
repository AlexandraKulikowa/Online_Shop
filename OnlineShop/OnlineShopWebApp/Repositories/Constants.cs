using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public static class Constants
    {
        public static string UserId = "UserId";
        public static Role RoleUser = new Role("Пользователь", "Может сделать заказ, сравнивать товары, пользоваться личным кабинетом");
    }
}