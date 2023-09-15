<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/Site.Master" 
AutoEventWireup="true" 
 CodeBehind="ProductDetails.aspx.cs" 
Inherits="shop.ProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <asp:FormView ID="productDetail" runat="server" 
    ItemType="shop.Models.Product" SelectMethod ="GetProduct" 
    RenderOuterTable="false">
         <ItemTemplate>
             <div>
                <h1><%#:Item.ProductName %></h1>
             </div>
             <br />
             <table>
                 <tr>
                <td>&nbsp;</td> 
                <td style="vertical-align: top; text-align:left;">
                <b>Price:</b>&nbsp;<%#: String.Format("{0:c}", 
                Item.Price) %>
                <br />
                <span><b>Dimension:</b>&nbsp;<%#:Item.Dimension 
                %></span>
                 <br />
                <span><b>Price Unit:</b>&nbsp;<%#:Item.Unit 
                %></span>
                 <br />
                    <a
                    href="/Buy.aspx?productCode=<%#:Item.ProductCode %>"> 
                     <span class="ProductListItem">
                     <b>Buy<b>
                     </span> 
                     </a>
                 </td>
                 </tr>
             </table>
         </ItemTemplate>
     </asp:FormView>
</asp:Content>