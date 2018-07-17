<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CustomerActiveManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CustomerProject.JinseBainian.CustomerActiveManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">

         function ShowUpdateWindows(KeyID, Control) {
             var Url = "CustomerActiveDetails.aspx?SourceKey=" + KeyID;
             $(Control).attr("id", "updateShow" + KeyID);
             showPopuWindows(Url, 500, 600, "a#" + $(Control).attr("id"));
         }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_Cma">
        <table class="table table_Condition">
            <tr>
                <td>套餐名称：
                    <asp:DropDownList runat="server" ID="ddlPackage" />
                </td>
                <td><HA:MyManager runat="server" ID="MyManager" Title="录入人："  /></td>
                <td>
                    套餐时段：
                    <asp:DropDownList runat="server" ID="ddlTimeSpan">
                        <asp:ListItem Text="请选择" Value="0"></asp:ListItem>
                        <asp:ListItem Text="午宴" Value="1"></asp:ListItem>
                        <asp:ListItem Text="晚宴" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    预定状态：
                    <asp:DropDownList runat="server" ID="ddlSchedule">
                        <asp:ListItem Text="请选择" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="预定" Value="0"></asp:ListItem>
                        <asp:ListItem Text="暂定" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;</td>
                <td>
                    <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="查询" OnClick="btnSearch_Click" />
                     <cc2:btnReload runat="server" ID="btnReload" />
                </td>
            </tr>
        </table>
        <table class="table table-bordered table-striped">
            <tr>
                <th style="width:180px">套餐名称</th>
                <th>套餐时段</th>
                <th>婚期</th>
                <th>录入人</th>
                <th>录入时间</th>
                <th>预定类型</th>
                <th>修改人</th>
                <th>修改时间</th>
                <th>操作</th>
            </tr>
            <asp:Repeater runat="server" ID="rptReserver" OnItemCommand="rptReserver_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%#GetByPackgetID(Eval("PackageID")) %></td>
                        <td><%#Eval("DateItem") %></td>
                        <td><%#Eval("PartyDate") %></td>
                        <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                        <td><%#Eval("CreateDate") %></td>
                        <td><%#Eval("State").ToString() == "0" ? "预定" : "暂定" %></td>
                        <td><%#GetEmployeeName(Eval("UpdateEmployee")) %></td>
                        <td><%#Eval("UpdateDate") %></td>
                        <td><%--<asp:Button runat="server" ID="btnLook" CssClass="btn btn-info" Text="查看" CommandName="LookDetails" CommandArgument='<%#Eval("SourceKey") %>' />--%>
                            <a href="#" onclick='ShowUpdateWindows(<%#Eval("SourceKey") %>,this);' class="SetState btn btn-primary">查看</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td>
                     <cc1:AspNetPagerTool ID="CtrPageIndex" OnPageChanged="CtrPageIndex_PageChanged" runat="server" PageSize="10"></cc1:AspNetPagerTool>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
