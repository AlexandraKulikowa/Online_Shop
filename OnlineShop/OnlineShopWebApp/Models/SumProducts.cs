namespace OnlineShopWebApp.Models
{
    public class SumProducts : Product
    {
        public int NumberOfProducts = 0;
        public Product Product;

        public SumProducts(string name, decimal cost, string description, GenreEnum genre, string paintingTechnique, Size size, int year, bool ispromo, int number) : base(name, cost, description, genre, paintingTechnique, size, year, ispromo)
        {
            NumberOfProducts = number;
        }
    }
}
