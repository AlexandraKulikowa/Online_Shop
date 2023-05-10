using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Comparison
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<Product> Products { get; set; }

        public Comparison()
        {
            Products = new List<Product>();
        }
    }
}