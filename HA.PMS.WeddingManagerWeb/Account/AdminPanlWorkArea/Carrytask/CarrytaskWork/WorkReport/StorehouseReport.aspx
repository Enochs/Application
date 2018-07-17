<%@ Page Title="库房领料单" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="StorehouseReport.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.WorkReport.StorehouseReport" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--startprint-->
    <table class="table table-bordered table-striped">
        <tr>
            <th style="width: 100px;">类别</th>
            <th style="width: 100px;">项目</th>
            <th style="width: 100px;">产品</th>
            <th style="width: 100px;">产品服务内容</th>
            <th style="width: 100px;">具体要求</th>
            <th style="width: 100px;">图片</th>

            <th style="width: 100px;">数量</th>


            <th style="width: 100px;">备注</th>
        </tr>
        <asp:Repeater ID="repWareHouseList" runat="server">
            <ItemTemplate>
                <tr>
                    <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("ParentCategoryName") %></td>
                    <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("CategoryName") %></td>
                    <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("ProductID") %></td>
                    <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("ServiceContent") %></td>
                    <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Requirement") %></td>
                    <td <%#GetBorderStyle(Eval("NewAdd")) %>><a href="#">查看资料</a></td>

                    <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Quantity") %></td>


                    <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Remark") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <!--endprint-->
    <div>
        <input type="button" value="打印" class="btn btn-primary" onclick="preview()" />
    </div>
</asp:Content>
