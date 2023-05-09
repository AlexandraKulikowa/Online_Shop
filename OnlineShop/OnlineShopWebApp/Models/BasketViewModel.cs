using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<BasketItemViewModel> ProductsInBasket { get; set; }
        public decimal TotalCost()
        {
            return ProductsInBasket?.Sum(x => x.Cost) ?? 0;
        }
        public decimal Amount()
        {
            return ProductsInBasket?.Sum(x => x.Amount) ?? 0;
        }
    }
}