<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FL_CustomersQueryDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.FL_CustomersQueryDetails" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc3" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

           // $("#<//%=txtStar.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
           //$("#<//%=txtEnd.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
       });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>新人明细</h5>
        </div>
        <div class="widget-content ">
            <a href="#" class="btn btn-primary  btn-mini" id="NotTelemarkting">未邀约</a>
            <a href="#" class="btn btn-primary  btn-mini" id="TelemarkingIng">邀约中</a>
            <a href="#" class="btn btn-primary  btn-mini" id="SuccessTelemark">邀约成功</a>
            <a href="#" class="btn btn-primary  btn-mini" id="TelemarkingRunOff">流失</a>
            <a href="#" class="btn btn-primary  btn-mini" id="newPersonDetails">新人明细</a>
            <a class="btn btn-primary  btn-mini" href="FL_CustomersCreate.aspx" id="createCustomers">录入新人信息 </a>
            <a class="btn btn-primary  btn-mini" id="TelemarkSum" href="#">邀约统计分析</a>

            <div class="widget-box">
                <div class="widget-content">

                    <table class="table table-bordered table-striped with-check">
                        <tr>
                            <td style="white-space: nowrap; width: 25%;">渠道类型</td>
                            <td style="white-space: nowrap; width: 25%;">
                                <cc3:ddlChannelType runat="server" ID="ddlFLChannelType"></cc3:ddlChannelType>
                            </td>
                            <td style="white-space: nowrap; width: 25%; text-align: right;">渠道名称:</td>
                            <td style="white-space: nowrap; width: 25%;">

                                <asp:DropDownList ID="ddlChannelName" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="white-space: nowrap;">新人状态:</td>
                            <td>
                                <asp:DropDownList ID="ddlCustomerStatus" runat="server"></asp:DropDownList>
                            </td>
                            <td style="text-align: right;">所有渠道联系人:</td>
                            <td>
                                <asp:DropDownList ID="ddlTactcontacts1" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="white-space: nowrap;">时间：</td>
                            <td>
                                <asp:DropDownList ID="ddlTimeChoose" runat="server">
                                    <asp:ListItem Text="请选择" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="接收时间" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="到店时间" Value="Todate"></asp:ListItem>
                                    <asp:ListItem Text="预定时间" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="婚期" Value="PartyDate"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right;">请选择:
                                <asp:TextBox ID="txtStar" onclick="WdatePicker();" runat="server"></asp:TextBox>
                            </td>
                            <td>至<asp:TextBox ID="txtEnd" onclick="WdatePicker();" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnQuery" OnClick="btnQuery_Click" CssClass="btn" Text="查找" runat="server" /></td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td></td>
                        </tr>

                    </table>
                </div>
            </div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>渠道名称</th>
                        <th>推荐人</th>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>新人状态</th>
                        <th>邀约人</th>
                        <th>邀约次数</th>
                        <th>门店</th>
                        <th>销售跟单人</th>
                        <th>到店时间</th>
                        <th>预定时间</th>
                        <th>定金</th>
                        <th>订单总额</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptQueryDetails" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#GetChannelTypeName(Eval("ChannelType")) %></td>
                                <td><%#Eval("Referee") %></td>
                                <td><%#Eval("Groom") %></td>
                                <td><%#Eval("GroomCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#Eval("CustomerStatus") %></td>
                                <td><%#GetEmployeeIdByName(Eval("CustomerID")) %></td>
                                <td><%#GetInviteCount(Eval("CustomerID")) %></td>
                                <td><%#Eval("StoreAddress") %></td>
                                <td><%#Eval("EmployeeName") %></td>
                                <td><%#GetDateStr(Eval("Todate")) %></td>
                                <td><%#GetDateStr(Eval("LastFollowDate")) %></td>
                                <td><%#Eval("EarnestMoney") %></td>
                                <td><%#GetAggregateAmount(Eval("CustomerID")) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="QueryDetailsPager" AlwaysShow="true" OnPageChanged="QueryDetailsPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoard runat="server" ClassType="FL_CustomersQueryDetails" ID="MessageBoard" />
        </div>
    </div>
</asp:Content>
