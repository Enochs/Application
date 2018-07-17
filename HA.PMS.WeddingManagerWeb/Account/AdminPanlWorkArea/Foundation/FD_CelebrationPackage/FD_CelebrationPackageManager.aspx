<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationPackageManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage.FD_CelebrationPackageManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_CelebrationPackageCreate.aspx?PackageID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 900, 1000, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            $("html").css({ "overflow-x": "hidden", "background-color": "transparent" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <a href="/AdminPanlWorkArea/Foundation/FD_CelebrationPackage/FD_CelebrationPackageCreate.aspx?NeedPopu=1&PackageType=0" class="btn btn-primary btn-mini">添加套系</a>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>套系名称</th>
                        <th>品牌</th>
                        <th>风格</th>
                        <th>套系价格</th>
                        <th>套系优惠价</th>
                        <th>创建时间</th>
                        <th>创建人</th>
                        <th>可预订数量</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCelebrationList" OnItemCommand="rptCelebrationList_ItemCommand" OnItemDataBound="rptCelebrationList_ItemDataBound" runat="server">
                        <ItemTemplate>
                            <tr skey='FD_CelebrationPackagePackageID<%#Eval("PackageID") %>'>
                                <td><%#Eval("PackageTitle") %></td>
                                <td><%#GetDepartmentBrandByDepartId(Eval("DepartmentID")) %></td>
                                <td><%#GetStyleByID(Eval("PackageSkip")) %></td>
                                <td><%#Eval("PackagePrice") %></td>
                                <td><%#Eval("PackagePreferentiaPrice") %></td>
                                <td><%#GetDateStr(Eval("PackageDate")) %></td>
                                <td><%#GetEmployeeName( Eval("CreateEmployee")) %></td>
                                <td><%#Eval("PackageSum") %></td>

                                <td><a href="FD_CelebrationPackageUpdate.aspx?PackageID=<%#Eval("PackageID") %>" class="btn btn-primary  btn-mini">修改套系</a>
                                    <a href="/AdminPanlWorkArea/QuotedPrice/SalePakegQuotedPriceCreateEdit.aspx?Kind=<%#Eval("PackageID") %>&NeedPopu=1" target="_blank" class="btn btn-primary  btn-mini">
                                        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></a>
                                    <asp:Button ID="lkbtnOff" CommandName="Off" Enabled="false" CommandArgument='<%#Eval("PackageID") %>' CssClass="btn btn-mini" runat="server" Text="启用"></asp:Button>
                                    <asp:Button ID="lkbtnOn" CommandName="On" Enabled="false" CommandArgument='<%#Eval("PackageID") %>' CssClass="btn btn-mini" runat="server" Text="禁用"></asp:Button>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="8">
                            <cc1:AspNetPagerTool ID="CelebrationPager" PageSize="10" AlwaysShow="true" OnPageChanged="CelebrationPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
