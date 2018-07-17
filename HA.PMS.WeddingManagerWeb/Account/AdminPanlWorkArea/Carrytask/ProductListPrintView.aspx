<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="ProductListPrintView.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.ProductListPrintView" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery.PrintArea.js"></script>
    <script type="text/javascript">
        function PrintNode(Control) {
            var mode = "popup";
            var close = mode == "popup";
            var options = { mode: mode, popClose: close };
            $("#Node" + Control).printArea(options);
        }
        function PrintWarHouseNode() {
            var mode = "popup";
            var close = mode == "popup";
            var options = { mode: mode, popClose: close };
            $("#WarHouseContent").printArea(options);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="Button1" type="button"  value="打印领料单" class="btn btn-primary btn-mini" onclick="PrintWarHouseNode('WarHouseContent');" />
    <a href="#" class="btn btn-primary btn-mini" onclick="window.close()">退出预览</a>
    <div id="WarHouseContent">
        <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle"/>
        <b>库房领料单</b>
        <table class="table table-bordered table-striped">
            <tr>
                <th>类别</th>
                <th>项目</th>
                <th>产品服务内容</th>
                <th>具体要求</th>
                <th>单价</th>
                <th>数量</th>
                <th>小计</th>
                <th>责任人</th>
                <th>备注</th>
            </tr>
            <asp:Repeater ID="repProductList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("ParentCategoryName") %></td>
                        <td><%#Eval("CategoryName") %></td>
                        <td><%#Eval("ServiceContent") %></td>
                        <td><%#Eval("Requirement") %></td>
                        <td><%#Eval("PurchasePrice") %></td>
                        <td><%#Eval("Quantity") %></td>
                        <td><%#Eval("Subtotal") %></td>
                        <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                        <td><%#Eval("Remark") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td style="font-family:Arial" colspan="9"><b>合计：</b><asp:Label ID="lblProductSumPrice" runat="server"></asp:Label>
                    <b>　　计划支出：</b><asp:Label ID="txtMoney" MaxLength="8" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
