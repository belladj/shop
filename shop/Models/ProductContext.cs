using Microsoft.Build.Framework.XamlTypes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace shop.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("shop")
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<TransactionHeader> TransactionHeaders { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
    }

}