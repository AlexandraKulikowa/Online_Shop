using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Views.Shared.Components.Basket
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IBasketRepository basketRepository;
        public BasketViewComponent(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }
        public IViewComponentResult Invoke()
        {
            var basket = basketRepository.TryGetByUserId(Constants.UserId);
            var productCounts = basket?.Amount() ?? 0;
            return View("Basket", productCounts);
        }
    }
}
