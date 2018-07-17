<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_SupplierProductSignIn.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SupplierProductSignIn" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        $("html").css({ "overflow-x": "hidden", "background-color": "transparent" });
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_ProductUpdate.aspx?ProductID=" + KeyID + "&IsSupplierShow=1";
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 450, 900, "a#" + $(Control).attr("id"));
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" style="height: 30px; border: 0px;">
                <table class="queryTable">
                    <tr>
                        <td>供应商：<asp:TextBox ID="txtSupplierName" runat="server"></asp:TextBox></td>
                        <td>产品名称：<asp:TextBox ID="txtProductName" runat="server"></asp:TextBox></td>
                        <td><asp:Button ID="BtnQuery" CssClass="btn btn-primary" runat="server" Text="查询" OnClick="BinderData" /></td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>供应商</th>
                        <th>产品名称</th>
                        <th>产品\服务描述</th>
                        <th>采购单价</th>
                        <th>销售单价</th>
                        <th>单位</th>
                        <th>说明</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptStorehouse" OnItemCommand="rptStorehouse_ItemCommand" runat="server">
                        <ItemTemplate>
                            <tr skey='FD_SaleSourcesProductID<%#Eval("ProductID") %>'>
                                <td><a href="FD_SupplierUpdate.aspx?OnlyView=1&SupplierID=<%#Eval("SupplierID") %>" target="_blank"><%#GetSupplierName(Eval("ProductID")) %> </a></td>
                                <td><%#Eval("ProductName") %></td>
                                <td><%#Eval("Specifications") %></td>
                                <td><%#Eval("ProductPrice") %></td>
                                <td><%#Eval("SalePrice") %></td>
                                <td><%#Eval("Unit") %></td>
                                <td><%#Eval("Explain") %></td>
                                <td><a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("ProductID") %>,this);'>修改</a>
                                    <asp:LinkButton ID="lkbtnDelete" Visible="false" CommandName="Delete" CssClass="btn btn-danger btn-mini" CommandArgument='<%#Eval("ProductID") %>' runat="server">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <cc1:AspNetPagerTool ID="StorePager" AlwaysShow="true" PageSize="8" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>
