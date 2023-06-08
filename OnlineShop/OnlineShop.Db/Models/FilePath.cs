
namespace OnlineShop.Db.Models
{
    public class FilePath
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public Product Product { get; set; }
    }
}
