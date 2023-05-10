using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Favourites
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<Product> Products { get; set; }

        public Favourites()
        {
            Products = new List<Product>();
        }
    }
}