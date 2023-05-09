using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<BasketItem> BasketItems { get; set; }

        public Basket()
        {
            BasketItems = new List<BasketItem>();
        }
    }
}