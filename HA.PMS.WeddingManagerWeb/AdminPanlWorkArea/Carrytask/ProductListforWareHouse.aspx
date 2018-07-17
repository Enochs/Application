<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductListforWareHouse.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.ProductListforWareHouse" Title="物料单" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script src="/Scripts/jquery.PrintArea.js"></script>
        <script type="text/javascript">
        function PrintNode(Control)
        {
            var mode = "popup";
            var close = mode == "popup";
            var options = { mode: mode, popClose: close };
            $("#Node"+Control).printArea(options);
        }

        function PrintWarHouseNode()
        {
            var mode = "popup";
            var close = mode == "popup";
            var options = { mode: mode, popClose: close };
            $("#WarHouseContent").printArea(options);
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>&nbsp;
    <div id="WarHouseContent">
        库房：<asp:Button ID="btnExportformWareHouse" runat="server" CssClass="btn btn-mini btn-primary" Text="导出到Excel" OnClick="btnExportformWareHouse_Click" />
        &nbsp;<table class="table table-bordered table-striped">
            <tr>
                <th style="width: 100px;">类别</th>
                <th style="width: 100px;">项目</th>
                <th style="width: 100px;">产品服务内容</th>
                <th style="width: 100px;">具体要求</th>
               <%-- <th style="width: 100px;">图片</th>--%>
                <th style="width: 100px;">单价</th>
                <th style="width: 100px;">数量</th>
                <th style="width: 100px;">小计</th>
                <th style="width: 100px;">责任人</th>
                <th style="width: 100px;">备注</th>
            </tr>
            <asp:Repeater ID="repWareHouseList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("ParentCategoryName") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("CategoryName") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("ServiceContent") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Requirement") %></td>
                        <%--<td <%#GetBorderStyle(Eval("NewAdd")) %>><a href="#">查看资料</a></td>--%>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("PurchasePrice") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Quantity") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Subtotal") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %> id="Partd<%#Container.ItemIndex %>">
                                <%# GetEmployeeName(Eval("EmpLoyeeID")) %>    </td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Remark") %></td>
                    </tr>
                </ItemTemplate>

            </asp:Repeater>
            <tr>
                <td colspan="9">合计：<asp:Label ID="lblWareSumPrice" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>
    </div>
    <div id="DivNewADD">
    </div>
    </asp:Content>

