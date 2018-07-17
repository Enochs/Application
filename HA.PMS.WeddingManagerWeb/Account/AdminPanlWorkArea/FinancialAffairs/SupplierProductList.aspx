<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierProductList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs.SupplierProductList" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" Title="供应商物料单" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div>
        <b>供应商:<asp:Label ID="lblKeyName" runat="server" Text='<%#Eval("KeyName") %>'></asp:Label>供应商电话：<%#Eval("Phone") %></b><table class="table table-bordered table-striped">
            <tr>
                <th>类别</th>
                <th>项目</th>
                <th>产品服务内容</th>
                <th>具体要求</th>
                <th>成本价</th>
                <th>数量</th>
                <th>小计</th>
                <th>备注</th>
            </tr>
            <asp:Repeater ID="repProductList" runat="server">
                <ItemTemplate>
                    <tr <%#Eval("ItemLevel").ToString()=="1"?"style=background-color:#FFCC99;":""%> <%#Eval("ItemLevel").ToString()=="2"?"style=background-color:#CCFFCC;":""%>>
                        <td><%#Eval("ParentCategoryName") %></td>
                        <td><%#Eval("CategoryName") %></td>
                        <td><%#Eval("ServiceContent") %></td>
                        <td><%#Eval("Requirement") %></td>
                        <td><%#Eval("PurchasePrice") %></td>
                        <td><%#Eval("Quantity") %></td>
                        <td><%#Eval("Subtotal") %></td>
                        <td><%#Eval("Remark") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        产品成本合计：<asp:Label ID="lblPriceSum" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
