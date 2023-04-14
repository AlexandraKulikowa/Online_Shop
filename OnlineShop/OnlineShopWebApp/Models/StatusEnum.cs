using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
        public enum StatusEnum
        {
        [Display(Name = "Открыт")]
        Open,
        [Display(Name = "В ожидании подтверждения")]
        Processing,
        [Display(Name = "Подтвержден")]
        Accepted,
        [Display(Name = "Оплачен")]
        Paid,
        [Display(Name = "Отправлен")]
        Shipped,
        [Display(Name = "Получен")]
        Delivered,
        [Display(Name = "Отменен")]
        Cancelled,
        [Display(Name = "Выполнен")]
        Completed
        }
}