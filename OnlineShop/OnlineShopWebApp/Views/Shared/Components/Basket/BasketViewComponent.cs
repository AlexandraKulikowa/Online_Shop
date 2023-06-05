using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShop.Db.Repositories;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Views.Shared.Components.Basket
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IBasketsRepository basketRepository;
        public BasketViewComponent(IBasketsRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basket = await basketRepository.TryGetByUserIdAsync(Constants.UserId);
            var basketVM = basket.ToBasketViewModel();
            var productCounts = basketVM?.Amount() ?? 0;
            return View("Basket", productCounts);
        }
    }
}