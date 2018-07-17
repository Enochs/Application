<%@ Page Title="" StylesheetTheme="Default" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="HandleLogList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.HandleLogList" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_Main">
        <table class="table table-bordered" style="width: 95%;">
            <tr>
                <td>日志类型:
                    <asp:DropDownList runat="server" ID="ddlHandleType" DataTextField="TypeName" DataValueField="TypeID"></asp:DropDownList>
                </td>
                <td>操作日期:</td>
                <td>
                    <HA:DateRanger runat="server" ID="DateRanger" />
                </td>
                <td>
                    <HA:MyManager runat="server" ID="MyManager" Title="操作人:" />
                </td>
            </tr>
            <tr>
                <td>关键字:<asp:TextBox runat="server" ID="txtPrimaryKey" Width="180px" /></td>
                <td colspan="3">
                    <asp:Button runat="server" ID="btnLook" Text="查询" CssClass="btn btn-primary" OnClick="btnLook_Click" />
                    <cc2:btnReload runat="server" ID="btnReLoad" />
                </td>
            </tr>
        </table>
        <table class="table table-bordered table-striped table-select">
            <tr>
                <td>编号</td>
                <td>类型</td>
                <td>IP</td>
                <td>内容</td>
                <td>网站URL</td>
                <td>操作日期</td>
                <td>操作人</td>
            </tr>
            <asp:Repeater runat="server" ID="rptHandleLog">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("HandleID") %></td>
                        <td><%#GetTypeByID(Eval("HandleType")) %></td>
                        <td><%#Eval("HandleIpAddress") %></td>
                        <td>
                            <asp:Label runat="server" ID="txtContent" Text='<%#Eval("HandleContent") %>' Style="white-space: normal; width: 20%;" /></td>
                        <td><a target="_blank" href='<%#Eval("HandleUrl") %>'>URL</a></td>
                        <td><%#Eval("HandleCreateDate") %></td>
                        <td><%#GetEmployeeName(Eval("HandleEmployeeID")) %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <td colspan="4">
                    <asp:Label runat="server" ID="lblDetailsCount" Style="color: red; font-size: 14px;" />
                </td>
            <tr>
                <td colspan="7">
                    <cc1:AspNetPagerTool ID="CtrPager" AlwaysShow="true" PageSize="10" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
                </td>
            </tr>

        </table>
    </div>
    <div class="div_ObjDataSource">
        <asp:ObjectDataSource ID="ObjHandleTypeDataSource" runat="server" SelectMethod="GetByAll" TypeName="HA.PMS.BLLAssmblly.Sys.HandleType"></asp:ObjectDataSource>
    </div>
</asp:Content>
