namespace OnlineShopWebApp.Models
{
    public class ProductWithQuantity : Product
    {
        public int NumberOfProducts = 0;

        public Product Product { get; set; }
        public ProductWithQuantity(string name, decimal cost, string description, GenreEnum genre, string paintingTechnique, Size size, int year, bool ispromo, int count) : base(name, cost, description, genre, paintingTechnique, size, year, ispromo)
        {
            NumberOfProducts = count;
        }
    }
}
