using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApp.Views.Shared.Components.Search
{
    public class SearchViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
