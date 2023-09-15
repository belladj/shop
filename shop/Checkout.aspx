<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" 
AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" 
Inherits="shop.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div id="ShoppingCartTitle" runat="server" class="ContentHead"><h1>Shopping 
    Cart</h1></div>
     <asp:GridView ID="CartList" runat="server" AutoGenerateColumns="False" 
    ShowFooter="True" GridLines="Vertical" CellPadding="4"
     ItemType="shop.Models.CartItem" 
    SelectMethod="GetCartItems" 
     CssClass="table table-striped table-bordered" > 
         <Columns>
             <asp:BoundField DataField="Product.ProductCode" HeaderText="Product Code" SortExpression="ProductCode"/> 
             <asp:BoundField DataField="Product.ProductName" HeaderText="Name" /> 
             <asp:BoundField DataField="Product.Price" HeaderText="Price (each)" 
            DataFormatString="{0:c}"/> 
             <asp:TemplateField HeaderText="Quantity"> 
                 <ItemTemplate>
                     <asp:TextBox ID="PurchaseQuantity" Width="40" 
                    runat="server" Text="<%#: Item.Quantity %>"></asp:TextBox> 
                      <%#:Item.Product.Unit %>
                 </ItemTemplate> 
             </asp:TemplateField> 
             <asp:TemplateField HeaderText="Subtotal"> 
                 <ItemTemplate>
                 <%#: String.Format("{0:c}", 
                ((Convert.ToDouble(Item.Quantity)) * 
                Convert.ToDouble(Item.Product.Price)))%>
                 </ItemTemplate> 
             </asp:TemplateField> 
             <asp:TemplateField HeaderText="Remove Item"> 
                 <ItemTemplate>
                 <asp:CheckBox id="Remove" runat="server"></asp:CheckBox>
                 </ItemTemplate> 
             </asp:TemplateField> 
         </Columns> 
     </asp:GridView>
     <div>
         <p></p>
         <strong>
             <asp:Label ID="LabelTotalText" runat="server" Text="Order Total: 
            "></asp:Label>
             <asp:Label ID="lblTotal" runat="server" 
            EnableViewState="false"></asp:Label>
         </strong> 
     </div>
     <br />
    <table> 
         <tr>
             <td>
             <asp:Button ID="UpdateBtn" runat="server" Text="Update" 
            OnClick="UpdateBtn_Click" />
             </td>
             <td>
             <asp:Button ID="ConfirmBtn" runat="server" Text="Confirm" 
            OnClick="ConfirmBtn_Click" />
             </td>
         </tr>
     </table>
</asp:Content>