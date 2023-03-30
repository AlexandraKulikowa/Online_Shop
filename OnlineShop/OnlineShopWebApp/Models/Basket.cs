using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class Basket
    {
        private static List<ProductWithQuantity> productsInBasket = new List<ProductWithQuantity>();
        public List<ProductWithQuantity> GetProducts()
        {
            return productsInBasket;
        }
        public void AddProduct(ProductWithQuantity product)
        {
            var selectedProduct = productsInBasket.Where(p => p.Id == product.Id).FirstOrDefault();
            if (selectedProduct != null)
            {
                selectedProduct.NumberOfProducts++;
            }
            else
            {
                productsInBasket.Add(product);
            }
        }
        public decimal TotalCost()
        {
            decimal result = 0; ;
            foreach (var product in productsInBasket)
            {
                result += product.Cost;
            }
            return result;
        }
    }
}
