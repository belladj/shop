using Microsoft.Build.Framework.XamlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shop.Models
{
    public class Product
    {
        [Key,Required, StringLength(18), Display(Name = "Product Code")]
        public string ProductCode { get; set; }
        [Required, StringLength(30), Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Price")]
        public double? Price { get; set; }
        public string Currency { get; set; }
        public double? Discount { get; set; }
        public string Dimension { get; set; }
        public string Unit { get; set; }
    }

}