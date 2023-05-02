using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class Comparison
    {
        public string UserId { get; set; }
        public List<Models.Product> Products { get; set; }
    }
}