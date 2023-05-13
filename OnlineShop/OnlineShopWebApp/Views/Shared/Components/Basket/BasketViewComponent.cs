using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Views.Shared.Components.Basket
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IBasketsRepository basketRepository;
        public BasketViewComponent(IBasketsRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }
        public IViewComponentResult Invoke()
        {
            var basket = basketRepository.TryGetByUserId(Constants.UserId);
            var basketVM = basket.ToBasketViewModel();
            var productCounts = basketVM?.Amount() ?? 0;
            return View("Basket", productCounts);
        }
    }
}