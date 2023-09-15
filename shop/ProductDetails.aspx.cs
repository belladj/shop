using shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shop
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Product> GetProduct([QueryString("productCode")] string code)
        {
            var _db = new shop.Models.ProductContext();
            IQueryable<Product> query = _db.Products;
            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(p => p.ProductCode == code);
            }
            else
            {
                query = null;
            }
            return query;
        }
    }

}