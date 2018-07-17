<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_SupplierProductDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SupplierProductDetails" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_ProductUpdate.aspx?ProductID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 450, 900, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            $(".grouped_elements").each(function (indexs, values) {
                if ($.trim($(this).html()) == "") {
                    $(this).remove();
                }
            });
            $(".grouped_elements").each(function (indexs, values) {
                var imgChildren = $(this).children("img");
                $(this).attr("href", imgChildren.attr("src"));
            });
            $("a.grouped_elements").fancybox();
            $("html").css({ "overflow-x": "hidden", "background-color": "transparent" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">
                <div class="widget-box" style="height: 30px; border: 0px;">
                    <table>
                        <tr>
                            <td>供应商：<asp:TextBox ID="txtSupplierName" runat="server"></asp:TextBox></td>
                            <td>产品名称：<asp:TextBox ID="txtProductName" runat="server"></asp:TextBox></td>
                            <td>产品类别：<cc2:CategoryDropDownList ID="ddlCategory" ParentID="0" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" runat="server"></cc2:CategoryDropDownList>
                                项目：<cc2:CategoryDropDownList ID="ddlProject" runat="server"></cc2:CategoryDropDownList></td>
                            <td><asp:Button ID="BtnQuery" CssClass="btn btn-primary" runat="server" Text="查询" OnClick="BinderData" /></td>
                        </tr>
                    </table>
                </div>
                <table class="table table-bordered table-striped table-select">
                    <thead>
                        <tr>
                            <th>供应商</th>
                            <th>产品名称</th>
                            <th>产品类别</th>
                            <th>项目</th>
                            <th>产品\服务描述</th>
                            <th>资料</th>
                            <th>采购单价</th>
                            <th>销售单价</th>
                            <th>单位</th>
                            <th>使用次数</th>
                            <th>说明</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptStorehouse" OnItemCommand="rptStorehouse_ItemCommand" OnItemDataBound="rptStorehouse_ItemDataBound" runat="server">
                            <ItemTemplate>
                                <tr skey='FD_SaleSourcesProductID<%#Eval("ProductID") %>'>
                                    <td><a href="FD_SupplierUpdate.aspx?OnlyView=1&SupplierID=<%#Eval("SupplierID") %>" target="_blank"><%#GetSupplierNameByProductID(Eval("ProductID")) %> </a></td>
                                    <td><a href="FD_SupplierUpdate.aspx?SupplierID=<%#Eval("SupplierID") %>" target="_blank"><%#Eval("ProductName") %></a></td>
                                    <td><%#GetCategoryName(Eval("Expr1")) %></td>
                                    <td><%#GetCategoryName(Eval("ProductProject")) %></td>
                                    <td><%#Eval("Specifications") %></td>
                                    <td><a class="grouped_elements" href="#" rel="group1"><asp:Image ID="imgStore" ImageUrl='<%#Eval("Data") %>' Width="100" Height="70" runat="server" /></a>
                                        <asp:LinkButton ID="lkbtnDownLoad" CssClass="btn btn-primary  " CommandArgument='<%#Eval("ProductID") %>' CommandName="DownLoad" runat="server"></asp:LinkButton>
                                    </td>
                                    <td><%#Eval("ProductPrice") %></td>
                                    <td><%#Eval("SalePrice") %></td>
                                    <td><%#Eval("Unit") %></td>
                                    <td><%#GetUsedCount(Eval("ProductID")) %></td>
                                    <td><%#Eval("Explain") %></td>
                                    <td style="width:86px">
                                        <a href="#" class="btn btn-primary  " onclick='ShowUpdateWindows(<%#Eval("ProductID") %>,this);'>修改</a>
                                        <asp:LinkButton ID="lkbtnDelete" CommandName="Delete" CssClass="btn btn-danger " CommandArgument='<%#Eval("ProductID") %>' runat="server">删除</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <cc1:AspNetPagerTool ID="StorePager"  AlwaysShow="true" PageSize="12" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>
