namespace OnlineShopWebApp.Models
{
    public class SumProducts : Product
    {
        public int NumberOfProducts;
        public Product Product;
        public SumProducts(Product product,int number)
        {
            this.Product = product;
            NumberOfProducts = number;
        }
    }
}
