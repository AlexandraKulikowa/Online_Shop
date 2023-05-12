using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Size
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsFrame { get; set; }
        public List<Product> Products { get; set; }
    }
}