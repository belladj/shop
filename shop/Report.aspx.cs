using shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shop
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<TransactionHeader> GetTransactionHeaders()
        {
            var _db = new ProductContext();
            IQueryable<TransactionHeader> query = _db.TransactionHeaders;
            return query;
        }

    }
}