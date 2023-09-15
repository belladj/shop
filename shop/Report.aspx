<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="shop.Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div id="ReportTitle" runat="server" class="ContentHead"><h1>Report</h1></div>
     <asp:GridView ID="report" runat="server" AutoGenerateColumns="False" 
    ShowFooter="True" GridLines="Vertical" CellPadding="4"
     ItemType="shop.Models.TransactionHeader" 
    SelectMethod="GetTransactionHeaders" 
     CssClass="table table-striped table-bordered" > 
         <Columns>
             <asp:TemplateField HeaderText="Transaction"> 
                 <ItemTemplate>
                 <%#:Item.DocumentCode%> - <%#Item.DocumentId%>
                 </ItemTemplate> 
             </asp:TemplateField> 
             <asp:BoundField DataField="User" HeaderText="User" /> 
             <asp:BoundField DataField="Total" HeaderText="Total" 
            DataFormatString="{0:c}"/> 
             <asp:BoundField DataField="Date" HeaderText="Date" /> 
         </Columns> 
     </asp:GridView>
</asp:Content>