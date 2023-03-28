namespace OnlineShopWebApp.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public SumProducts Sumproduct { get; set; }
        public Order(int orderId, int userId, SumProducts sumproduct)
        {
            OrderId = orderId;
            UserId = userId;
            Sumproduct = sumproduct;
        }
    }
}
