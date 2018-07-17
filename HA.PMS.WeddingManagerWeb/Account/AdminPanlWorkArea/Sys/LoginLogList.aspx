<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" StylesheetTheme="Default" AutoEventWireup="true" CodeBehind="LoginLogList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.LoginLogList" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_main">
        <table class="table_look" cellspacing="10" cellpadding="10">
            <tr>
                <td>
                    <HA:MyManager Title="责任人：" runat="server" ID="MyManager" />
                </td>
                <td>
                    <HA:DateRanger Title="登录日期：" runat="server" ID="DateRanger" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnLook" Text="查询" CssClass="btn btn-primary" OnClick="BinderData" />
                    <cc2:btnReload runat="server" ID="btnReload" />
                </td>
            </tr>
        </table>
        <table class="table table-bordered table-selected">
            <tr>
                <th>编号</th>
                <th>姓名</th>
                <th>登陆地址</th>
                <th>登陆时间</th>
            </tr>
            <asp:Repeater runat="server" ID="rptLoginLogList">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("Id") %></td>
                        <td><%#GetEmployeeName(Eval("EmployeeId")) %></td>
                        <td><%#Eval("IpAddress") %></td>
                        <td><%#Eval("CreateDate") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="4">
                    <asp:Label runat="server" ID="lblDetailsCount" Style="color: red; font-size: 14px;" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <cc1:AspNetPagerTool ID="CtrPager" AlwaysShow="true" PageSize="10" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:LinkButton runat="server" ID="lbtnTenNum" Text="每页显示10条"  OnClick="lbtnTenNum_Click"/>
                    <asp:LinkButton runat="server" ID="lbtnTwentyNum" Text="每页显示20条" OnClick="lbtnTenNum_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
