using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shop.Models
{
    public class TransactionDetail
    {
        public string DocumentCode { get; set; }
        [Key]
        public string DocumentId { get; set; }
        public string ProductCode { get; set; }
        public double? Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public double? Subtotal { get; set; }
        public string Currency { get; set; }

    }
}