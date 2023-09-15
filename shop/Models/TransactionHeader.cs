using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shop.Models
{
    public class TransactionHeader
    {
        public string DocumentCode { get; set; }
        [Key]
        public string DocumentId { get; set; }
        public string User { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public List<TransactionDetail> ItemList { get; set; }
    }
}