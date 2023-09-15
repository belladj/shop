using shop.Actions;
using shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Collections;
using System.Web.ModelBinding;

namespace shop
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (CartActions usersShoppingCart = new CartActions())
            {
                decimal cartTotal = 0;
                cartTotal = usersShoppingCart.GetTotal();
                if (cartTotal > 0)
                {
                    lblTotal.Text = String.Format("{0:c}", cartTotal);
                }
                else
                {
                    LabelTotalText.Text = "";
                    lblTotal.Text = "";
                    ShoppingCartTitle.InnerText = "No Items";
                    UpdateBtn.Visible = false;
                    ConfirmBtn.Visible = false;
                }
            }
        }

        public List<CartItem> GetCartItems()
        {
            CartActions actions = new CartActions();
            return actions.GetCartItems();
        }

        public List<CartItem> UpdateCartItems()
        {
            using (CartActions usersShoppingCart = new CartActions())
            {
                String cartId = usersShoppingCart.GetCartId();
                CartActions.ShoppingCartUpdates[] cartUpdates = new
               CartActions.ShoppingCartUpdates[CartList.Rows.Count];
                for (int i = 0; i < CartList.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(CartList.Rows[i]);
                    cartUpdates[i].ProductCode = Convert.ToString(rowValues["Product.ProductCode"]);
                    CheckBox cbRemove = new CheckBox();
                    cbRemove = (CheckBox)CartList.Rows[i].FindControl("Remove");
                    cartUpdates[i].RemoveItem = cbRemove.Checked;
                    TextBox quantityTextBox = new TextBox();
                    quantityTextBox =
                   (TextBox)CartList.Rows[i].FindControl("PurchaseQuantity");
                    cartUpdates[i].PurchaseQuantity =
                   Convert.ToInt16(quantityTextBox.Text.ToString());
                }
                usersShoppingCart.UpdateShoppingCartDatabase(cartId, cartUpdates);
                CartList.DataBind();
                lblTotal.Text = String.Format("{0:c}", usersShoppingCart.GetTotal());
                return usersShoppingCart.GetCartItems();
            }
        }
        public static IOrderedDictionary GetValues(GridViewRow row)
        {
            IOrderedDictionary values = new OrderedDictionary();
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.Visible)
                {
                    cell.ContainingField.ExtractValuesFromCell(values, cell,
                   row.RowState, true);
                }
            }
            return values;
        }
        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateCartItems();
        }
        protected void ConfirmBtn_Click(object sender, EventArgs e)
        {
            using (CartActions usersShoppingCart = new
               CartActions())
            {
                usersShoppingCart.CreateRecord();
                usersShoppingCart.EmptyCart();
            }
            Response.Redirect("Report.aspx");
        }
    }
}