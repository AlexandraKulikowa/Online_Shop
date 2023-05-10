using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class ComparisonViewModel
    {
        public string UserId { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}