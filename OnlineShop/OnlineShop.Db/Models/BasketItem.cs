using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class BasketItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public Basket Basket { get; set; }
        public void ChangeAmount(bool sign)
        {
            if (sign)
                Amount++;
            else
                Amount--;
        }
    }
}