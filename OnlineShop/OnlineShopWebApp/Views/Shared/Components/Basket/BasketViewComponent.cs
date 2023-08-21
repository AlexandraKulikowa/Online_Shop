using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Helpers;
using OnlineShop.Db.Repositories;
using System.Threading.Tasks;
using OnlineShop.Db;

namespace OnlineShopWebApp.Views.Shared.Components.Basket
{
    public class BasketViewComponent : ViewComponent
    {
        IUnitOfWork unitOfWork;
        public BasketViewComponent(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basket = await unitOfWork.BasketsDbRepository.TryGetByUserIdAsync(Constants.UserId);
            var basketVM = basket.ToBasketViewModel();
            var productCounts = basketVM?.Amount() ?? 0;
            return View("Basket", productCounts);
        }
    }
}