using OnlineShop.Db.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class Favourites
    {
        public string UserId { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}