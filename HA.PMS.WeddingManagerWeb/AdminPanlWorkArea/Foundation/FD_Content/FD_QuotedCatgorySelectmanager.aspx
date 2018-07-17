<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FD_QuotedCatgorySelectmanager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.FD_QuotedCatgorySelectmanager" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
     <table>
        <tr style="vertical-align: top; text-align: left;">
            <td>
                <table id="TablerepProductByCatogryforWarehouseList" border="1" cellpadding="5" cellspacing="1" class="Productdiv">
                    <tr>
                 
                        <td>产品</td>

                        <td>价格</td>
                        <td>数量</td>

                    </tr>
                    <asp:Repeater ID="repProductByCatogryforWarehouseList" runat="server">
                        <ItemTemplate>
                            <tr>
             
                                <td bgcolor="#FFFFEE" class="ProductName" style="width: 100px;"><%#Eval("ProductName") %></td>

                                <td><%#Eval("SalePrice") %></td>
                                <td>原数量:<%#GetCount(Container.ItemIndex) %> <asp:TextBox ID="txtSaleCount" runat="server"></asp:TextBox>
 
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Button ID="btnSaveSelect" runat="server" ClientIDMode="Static" CssClass="btn btn-success" OnClick="btnSaveSelect_Click" Text="保存" />
            </td>
        </tr>
    </table>

</asp:Content>

 
