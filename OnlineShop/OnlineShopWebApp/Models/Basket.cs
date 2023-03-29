using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class Basket
    {
        private static List<SumProducts> productsInBasket = new List<SumProducts>();
        public List<SumProducts> GetProducts()
        {
            return productsInBasket;
        }
    }
}
