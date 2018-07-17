<%@ Page Title="每日汇总" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="WorkReportByDay.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.WorkReportByDay" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .table thead tr th {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="div_Main" style="height: 800px; overflow: auto;">
        <div class="ui-menu-divider">
            <table class="table">
                <tr>
                    <td runat="server" id="td_Dep">部门</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <cc2:DepartmentDropdownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                </cc2:DepartmentDropdownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td runat="server" id="td_Emp">责任人</td>
                    <td>
                        <%--<HA:MyManager runat="server" ID="MyManager" Title="责任人" />--%>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList runat="server" ID="ddlEmployee" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" Title="时间" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnLookFor" Text="查询" OnClick="btnLookFor_Click" CssClass="btn btn-primary" />
                        <cc2:btnReload runat="server" ID="btnReload" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select" style="width: 95%;">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th>
                            <asp:LinkButton runat="server" ID="lbtnNewCreate" Text="新录入" OnClick="lbtnNewCreate_Click" /></th>
                        <th>
                            <asp:LinkButton runat="server" ID="lbtnInviteNum" Text="电销量" OnClick="lbtnNewCreate_Click" /></th>
                        <th>
                            <asp:LinkButton runat="server" ID="lbtnLoseInviteNum" Text="流失(邀约中)" OnClick="lbtnNewCreate_Click" /></th>
                        <th>
                            <asp:LinkButton runat="server" ID="lbtnInviteSuccess" Text="邀约成功" OnClick="lbtnNewCreate_Click" /></th>
                        <th>
                            <asp:LinkButton runat="server" ID="lbtnOrderNum" Text="跟单量" OnClick="lbtnNewCreate_Click" /></th>
                        <th>
                            <asp:LinkButton runat="server" ID="lbtnLoseOrderNum" Text="流失量(跟单中)" OnClick="lbtnNewCreate_Click" /></th>
                        <th>
                            <asp:LinkButton runat="server" ID="lbtnOrderSuccessNum" Text="成功预定" OnClick="lbtnNewCreate_Click" /></th>
                        <th>
                            <asp:LinkButton runat="server" ID="lbtnQuotedCheckNum" Text="已签约" OnClick="lbtnNewCreate_Click" /></th>
                        <th>
                            <asp:LinkButton runat="server" ID="lbtnFinishAmount" Text="现金流" OnClick="lbtnNewCreate_Click" /></th>
                        <th>
                            <asp:LinkButton runat="server" ID="lbtnOrderamount" Text="订单金额" OnClick="lbtnNewCreate_Click" /></th>
                        <th>记录时间</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="repWorkReport">
                        <ItemTemplate>
                            <tr>
                                <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                                <td><%#Eval("CreateNum") %></td>
                                <td><%#Eval("InviteNum") %></td>
                                <td><%#Eval("LoseInviteNum") %></td>
                                <td><%#Eval("InviteSuccessNum") %></td>
                                <td><%#Eval("OrderNum") %></td>
                                <td><%#Eval("LoseOrderNum") %></td>
                                <td><%#Eval("OrderSuccessNum") %></td>
                                <td><%#Eval("QuotedCheckNum") %></td>
                                <td><%#Eval("FinishAmount") %></td>
                                <td><%#Eval("OrderAmount") %></td>
                                <td><%#Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td>合计</td>
                        <td><%=GetSumByToday() %></td>
                        <td>
                            <table>
                                <tr>
                                    <td>客户数量:</td>
                                    <td><%=GetInviteSums() %></td>
                                </tr>
                                <tr>
                                    <td>沟通次数:</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblInviteSum" /></td>
                                </tr>
                            </table>
                        </td>
                        <td><%=GetInviteSum(7) %></td>
                        <td><%=GetInviteSum(6) %></td>
                        <td>
                            <table>
                                <tr>
                                    <td>客户数量:</td>
                                    <td><%=GetOrderSums() %></td>
                                </tr>
                                <tr>
                                    <td>沟通次数:</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblOrderSum" /></td>
                                </tr>
                            </table>
                        </td>

                        <td><%=GetOrderSum(10) %></td>
                        <td><%=GetOrderSum(13) %></td>
                        <td><%=GetQuotedSum(15 ) %></td>
                        <td><%=GetFinishAmountSum() %></td>
                        <td><%=GetOrderAmountSum() %></td>
                        <td></td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>

</asp:Content>
