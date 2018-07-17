<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseMarkProductsDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseMarkProductsDetails" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
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
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    库房：<asp:LinkButton Visible="false" ID="btnExportformWareHouse" CssClass="btn btn-primary" runat="server" Text="导出到Excel" OnClick="btnExportformWareHouse_Click" />
    <a href="#" class="btn btn-primary" onclick="PrintWarHouseNode()">打印</a>
    <a href="#" class="btn btn-primary" onclick="window.close()">关闭</a>
    <div id="WarHouseContent">
        <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
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
            <asp:Repeater ID="repWareHouseList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td <%#GetNewAddStyle(Eval("NewAdd")) %>><%#Eval("ParentCategoryName") %></td>
                        <td <%#GetNewAddStyle(Eval("NewAdd")) %>><%#Eval("CategoryName") %></td>
                        <td <%#GetNewAddStyle(Eval("NewAdd")) %>><%#Eval("ServiceContent") %></td>
                        <td <%#GetNewAddStyle(Eval("NewAdd")) %>><%#Eval("Requirement") %></td>
                        <td <%#GetNewAddStyle(Eval("NewAdd")) %>><%#Eval("PurchasePrice") %></td>
                        <td <%#GetNewAddStyle(Eval("NewAdd")) %>><%#Eval("Quantity") %></td>
                        <td <%#GetNewAddStyle(Eval("NewAdd")) %>><%#Eval("Subtotal") %></td>
                        <td <%#GetNewAddStyle(Eval("NewAdd")) %>><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                        <td <%#GetNewAddStyle(Eval("NewAdd")) %>><%#Eval("Remark") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="9"><b>合计：<asp:Label ID="lblWareSumPrice" runat="server" Text=""></asp:Label></b></td>
            </tr>
        </table>
    </div>
</asp:Content>
