
namespace OnlineShopWebApp.Models
{
    public class Product
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string PaintingTechnique { get; set; }
        public string Size { get; set; }
        public int Year { get; set; }
        public bool IsPromo { get; set; }

        public Product(int id,string name, decimal cost, string description, string genre,  string paintingTechnique, string size, int year, bool ispromo)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Description = description;
            Genre = genre;
            PaintingTechnique = paintingTechnique;
            Size = size;
            Year = year;
            IsPromo = ispromo;
        }
        public override string ToString() 
        {
            return $"{Id} \n{Name} \n{Cost} \n\n";
        }
}
}
