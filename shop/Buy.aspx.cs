using shop.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shop
{
    public partial class Buy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rawId = Request.QueryString["productCode"];
            if (!String.IsNullOrEmpty(rawId))
            {
                using (CartActions usersShoppingCart = new
               CartActions())
                {
                    usersShoppingCart.AddToCart(rawId);
                }
            }
            else
            {
                Debug.Fail("ERROR : We should never get to Buy.aspx without a ProductCode.");
               
                throw new Exception("ERROR : It is illegal to load Buy.aspx without setting a ProductCode.");
            }
            Response.Redirect(Request.UrlReferrer.ToString());
        }

    }
}