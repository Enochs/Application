<%@ Page Title="" Language="C#" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CompanyJurisdiction.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.CompanyJurisdiction" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ui-menu-divider">
        <table>
            <tr>
                <td>姓名:</td>
                <td>

                    <asp:TextBox runat="server" ID="txtUserName" /></td>
                <td>部门:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlDepartment" /></td>
                <td>状态</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSates">
                        <asp:ListItem Text="请选择" Value="0" />
                        <asp:ListItem Text="启用" Value="2" />
                        <asp:ListItem Text="禁用" Value="3" />
                    </asp:DropDownList></td>
                <td>
                    <asp:Button runat="server" ID="btnLookFor" Text="查询" CssClass="btn btn-primary" OnClick="btnLookFor_Click" />
                    <cc2:btnReload runat="server" ID="btnReload" />
                </td>
            </tr>
        </table>
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th>员工ID</th>
                    <th>姓名</th>
                    <th>帐号</th>
                    <th>部门</th>
                    <th>性别</th>
                    <th>创建时间</th>
                    <th>手机</th>
                    <th>状态</th>
                    <th>操作</th>
                    <th>测试</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="rptUserJurisdiction" OnItemCommand="rptUserJurisdiction_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("EmployeeID") %>
                                <asp:HiddenField runat="server" ID="hideEmployeeID" Value='<%#Eval("EmployeeID") %>' />
                            </td>
                            <td><%#Eval("EmployeeName") %></td>
                            <td><%#Eval("LoginName") %></td>
                            <td><%#GetDepartmentName(Eval("DepartmentID")) %></td>
                            <td><%#Eval("Sex").ToString() == "1" ? "女" : "男" %></td>
                            <td><%#Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
                            <td><%#Eval("CellPhone") %></td>
                            <td><%#Eval("EmployeeTypeID").ToString().ToInt32() <=2 ? "启用" : "禁用" %></td>
                            <td>
                                <%--<asp:LinkButton runat="server" ID="lbtnAllow1" CommandName="Allow1" CommandArgument='<%#Eval("EmployeeID") %>' Text="启用" CssClass="btn btn-primary btn-mini" />--%>
                                <asp:LinkButton runat="server" ID="lbtnAllow" CommandName="Allow" CommandArgument='<%#Eval("EmployeeID") %>' Text="启用" CssClass="btn btn-primary btn-mini" />
                                <asp:LinkButton runat="server" ID="lbtnDeny" CommandName="Deny" CommandArgument='<%#Eval("EmployeeID") %>' Text="禁用" CssClass="btn btn-primary btn-mini" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="7">
                        <cc1:AspNetPagerTool ID="CtrPageIndex" AlwaysShow="true" PageSize="10" OnPageChanged="CtrPageIndex_PageChanged" runat="server"></cc1:AspNetPagerTool>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>
