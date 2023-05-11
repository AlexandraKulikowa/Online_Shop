namespace OnlineShop.Db.Models
{
    public class Favourites
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Product Product { get; set; }

    }
}