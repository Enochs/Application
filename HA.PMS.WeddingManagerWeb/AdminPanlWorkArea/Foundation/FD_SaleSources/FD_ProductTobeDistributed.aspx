<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_ProductTobeDistributed.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_ProductTobeDistributed" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowWindowsCreate(KeyID, Control) {
            var Url = "FD_SupplierCreate.aspx?ProductID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 800, 900, "a#" + $(Control).attr("id"));
        }
        function ShowWindowsCreate(KeyID, Control) {
            var Url = "FD_SupplierCreate.aspx?ProductID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 800, 900, "a#" + $(Control).attr("id"));
        }

        //弹出框选择类别
        function ShowWindowsSelect(KeyID, Control, Type) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectCategoryProject.aspx?Keys=" + KeyID + "&Type=" + Type;
            $(Control).attr("id", "selectShow" + KeyID);
            showPopuWindows(Url, 454, 900, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {
            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable td").css({ "border": "0", "vertical-align": "top" });
            $("html").css({ "overflow-x": "hidden" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table class="table table-bordered table-striped table-select" style="width: 100%;">
                <thead>
                    <tr id="trContent">
                        <th>供应商名称</th>
                        <th>产品名称</th>
                        <th>产品类别</th>
                        <th>项目</th>
                        <th>规格尺寸</th>
                        <th>资料</th>
                        <th>采购单价</th>
                        <th>销售单价</th>
                        <th>单位</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody id="tbyContent">
                    <asp:Repeater ID="rptProductTobeDistributed" runat="server">
                        <ItemTemplate>
                            <tr skey='<%#Eval("Keys") %>'>
                                <td>
                                    <asp:Literal ID="ltlSupplierName" Text='<%#Eval("SupplierName") %>' runat="server"></asp:Literal></td>
                                <td><%#Eval("ProductName") %></td>
                                <td>
                                    <asp:HiddenField ID="hfProductCategory" Value='<%#Eval("ProductCategory") %>' runat="server" />
                                    <%#GetCategoryName(Eval("ProductCategory")) %>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hfProjectCategory" Value='<%#Eval("ProjectCategory") %>' runat="server" />
                                    <%#GetCategoryName(Eval("ProjectCategory")) %></td>
                                <td><%#Eval("Explain") %></td>
                                <td><%#Eval("Data") %></td>
                                <td>
                                    <asp:TextBox Style="padding: 0; margin: 0" ID="txtPurchasePrice" MaxLength="8" Width="70" Text='<%#Eval("PurchasePrice") %>' runat="server"></asp:TextBox></td>
                                <td><%#Eval("SalePrice") %></td>
                                <td><%#Eval("Unit") %></td>
                                <td style="width: 250px">
                                    <a href="#" class="btn btn-primary btn-mini" onclick="ShowWindowsSelect(<%#Eval("Keys") %>,this,'Store');">直接入库</a>
                                    <a href="#" class="btn btn-primary btn-mini" onclick="ShowWindowsSelect(<%#Eval("Keys") %>,this,'Quoted');">直接入报价单</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="TobeDistributedPager" runat="server" AlwaysShow="true"></cc1:AspNetPagerTool>

            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
