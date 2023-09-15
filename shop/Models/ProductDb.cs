using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace shop.Models
{
    public class ProductDb : DropCreateDatabaseAlways<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            GetProducts().ForEach(p => context.Products.Add(p));
        }

        private static List<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    ProductCode = "SKPW",
                    ProductName = "SO Klin Pewangi",
                    Price = 15000,
                    Currency = "IDR",
                    Discount = 10,
                    Dimension = "13 x 10 cm",
                    Unit = "PCS"
                 },
                new Product
                {
                    ProductCode = "GB",
                    ProductName = "GIV Biru",
                    Price = 11000,
                    Currency = "IDR",
                    Discount = 0,
                    Dimension = "15 x 12 cm",
                    Unit = "PCS"
                 },
                new Product
                {
                    ProductCode = "SKL",
                    ProductName = "SO Klin Liquid",
                    Price = 18000,
                    Currency = "IDR",
                    Discount = 0,
                    Dimension = "20 x 10 cm",
                    Unit = "PCS"
                 }
            };
            return products;
        }
    }
}