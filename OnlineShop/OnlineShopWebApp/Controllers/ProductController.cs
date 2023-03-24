using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private static ProductRepository products;
        public ProductController()
        {
            products = new ProductRepository();
        }
        public string Index(int id)
        {
            var product = products.TryGetById(id);
            if (product == null)
            {
                return $"Сожалеем. Картина с идентификатором {id} отсутствует.";
            }
            return product.ToStringFullInfo();
        }
    }
}
