<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CustomerInfoModifyManger.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Customer.CustomerInfoModifyManger" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<%@ Register Src="../../Control/MyManager.ascx" TagName="MyManager" TagPrefix="uc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/ecmascript">
        function ShowUpdateWindows(KeyID, Control) {;
            var Url = "/AdminPanlWorkArea/Sys/Customer/CustomerInfoModify.aspx?CustomerID=" + KeyID;
            showPopuWindows(Url, "100%", "100%", "a#modifythis");
            $("#modifythis").click();
        }
    </script>
    <style type="text/css">
        #tbl_msg tr td {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <a href="#" style="display: none" id="modifythis">修改信息</a>
        <table style="margin-top: 10px; margin-left: 15px;">
            <tr>
                <td>
                    <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
                </td>
                <td>电话：<asp:TextBox ID="txtBrideCellPhone" runat="server" MaxLength="20"></asp:TextBox></td>
                <td>时间：<asp:DropDownList Width="85" ID="ddltimerType" runat="server">
                    <asp:ListItem Value="-1">请选择</asp:ListItem>
                    <asp:ListItem Value="0">婚期</asp:ListItem>
                    <asp:ListItem Value="1">录入时间</asp:ListItem>
                    <asp:ListItem Value="2">到店时间</asp:ListItem>
                </asp:DropDownList>
                </td>
                <td>
                    <HA:DateRanger runat="server" ID="DateRanger" />
                </td>
                <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server" Width="100px"></cc2:ddlHotel>
                    <asp:TextBox runat="server" ID="txtHotel" onk />
                </td>
            </tr>
            <tr>
                <td>
                    <%--                    <asp:DropDownList Width="85" ID="ddlEmployee" runat="server">
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="0">责任人</asp:ListItem>
                        <asp:ListItem Value="1">婚礼顾问</asp:ListItem>
                        <asp:ListItem Value="2">策划师</asp:ListItem>
                    </asp:DropDownList>--%>
                    <asp:RadioButtonList runat="server" ID="rblEmployee" RepeatDirection="Horizontal" CellPadding="8" CellSpacing="8">
                        <asp:ListItem Text="责任人" Value="0" Selected="True" />
                        <asp:ListItem Text="电销" Value="1" />
                        <asp:ListItem Text="录入人" Value="2" />
                        <asp:ListItem Text="策划师" Value="3" />
                    </asp:RadioButtonList>
                </td>
                <td>
                    <uc1:MyManager ID="MyManager1" runat="server" Title="" />
                </td>
                <td>新人状态<cc2:ddlCustomersState runat="server" ID="ddlCustomersState1"></cc2:ddlCustomersState></td>
                <td>
                    <asp:Button ID="BtnQuery" OnClick="BtnQuery_Click" CssClass="btn btn-primary" runat="server" Text="查询" />
                    <cc2:btnReload ID="btnReload2" runat="server" />
                </td>
            </tr>
        </table>
        <table id="tbl_msg" class="table table-bordered table-striped table-select">
            <thead>
                <tr>
                    <th style="display: none">渠道名称</th>
                    <th style="display: none">推荐人</th>
                    <th>新人姓名</th>
                    <th>联系电话</th>
                    <th>婚期</th>
                    <th>录入时间</th>
                    <th>时段</th>
                    <th>酒店</th>
                    <th>状态</th>
                    <th>录入人</th>
                    <th>责任人(电销)</th>
                    <th>顾问</th>
                    <th>策划师</th>
                    <th>说明</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptCustomer" runat="server">
                    <ItemTemplate>
                        <tr skey='<%#Eval("CustomerID") %>'>
                            <td style="display: none"><%#Eval("Channel") %></td>
                            <td style="display: none"><%#Eval("Referee") %></td>
                            <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("Bride").ToString() ==  "" ? "无" : Eval("Bride") %>/<%#Eval("Groom").ToString() ==  "" ? "无" : Eval("Groom") %></a></td>

                            <td><%#Eval("ContactPhone") %></td>
                            <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                            <td><%#string.Format("{0:yyyy-MM-dd}",Eval("RecorderDate")) %></td>
                            <td><%#Eval("TimeSpans") %></td>
                            <td><%#Eval("Wineshop") %></td>
                            <td><%#GetCustomerStateStr(Eval("State")) %></td>
                            <td><%#GetEmployeeName(Eval("CreateEmployee")) %></td>
                            <td>
                                <asp:Label runat="server" ID="lblDutyEmployee" ToolTip="责任人(邀约人)"><%#GetEmployeeName(Eval("DutyEmployee")) %> (<%#GetEmployeeName(Eval("InviteEmployee")) %>)</asp:Label></td>
                            <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                            <td><%--<%#GetEmployeeName(Eval("QuotedEmployee")) %>--%><%#GetQuotedEmployee(Eval("CustomerID")) %></td>
                            <td><span style="cursor: default" title='<%#Eval("Other") %>'><%#ToInLine(Eval("Other")) %></span></td>
                            <td><a href="/AdminPanlWorkArea/Sys/Customer/CustomerInfoModify.aspx?CustomerID=<%#Eval("CustomerID") %>" class="btn btn-primary btn-mini">修改</a>

                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <cc1:AspNetPagerTool ID="CustomersPager" AlwaysShow="true" PageSize="10" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
    </div>
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-align-justify"></i></span>
            <h5>操作栏</h5>
        </div>
        <div class="widget-content nopadding" id="paretntSelect">
            <table>
                <tr>
                    <td>
                        <HA:CstmNameSelector runat="server" ID="CstmSelector" Title="新人姓名:" />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblTelphone" Text="联系电话:" /></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTelphone" /></td>
                    <td>
                        <asp:Button runat="server" ID="btnSetups" Text="确定" CssClass="btn btn-primary" OnClick="btnSetups_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
