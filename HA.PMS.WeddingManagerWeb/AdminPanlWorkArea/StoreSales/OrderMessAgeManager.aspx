<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderMessAgeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.OrderMessAgeManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" Title="上级辅导意见" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Src="../Control/SetIntiveSerch.ascx" TagName="SetIntiveSerch" TagPrefix="uc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="uc1" TagName="MessageBoard" %>

<asp:Content ContentPlaceHolderID="head" runat="server" ID="Content2">
    <script type="text/javascript">


        function ShowOrderMessAge(OrderID) {
            var URI = "OrderMessAge.aspx?OrderID=" + OrderID
            showPopuWindows(URI, 700, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">

    <uc1:SetIntiveSerch ID="SerchControl" runat="server" />
        <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <br />
 <table class="table table-bordered table-striped with-check">
        <tr>
            <th>渠道名称</th>
            <th>渠道类型</th>
            <th>新人姓名</th>
            <th>联系电话</th>
            <th>婚期</th>
            <th>酒店</th>
            <th>录入日期</th>
            <th>录入人</th>
            <th>新人状态</th>
            <th>邀约负责人</th>
            <th>沟通记录</th>
        </tr>
        <asp:Repeater ID="repCustomer" runat="server">
            <ItemTemplate>

                <tr>
                    <td><%#Eval("Channel") %></td>
                    <td><%#Eval("ChannelType") %></td>
                    <td><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></td>
                    <td><%#Eval("BrideCellPhone") %></td>
                    <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                    <td><%#Eval("Address") %></td>
                    <td><%#Eval("Wineshop") %></td>
                    <td><%#GetEmployeeName(Eval("Recorder")) %></td>
                    <td><%#Eval("State") %></td>
                    <td><%#Eval("Operator") %></td>
                    <td>
                        <a href="#" onclick="return ShowOrderMessAge(<%#Eval("OrderID") %>)">辅导意见</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="11">
                <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server"   OnPageChanged="CtrPageIndex_PageChanged">
                </cc1:AspNetPagerTool>
            </td>

        </tr>
    </table>
    <uc1:MessageBoard runat="server" ClassType="OrderMessAgeManager" ID="MessageBoard" />
</asp:Content>
