<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPricefileManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPricefileManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Upload").each(function () { showPopuWindows($(this).attr("href"), 720, 700, $(this)); });
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table>
                <tr>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" />
                    </td>
                    <td>新人姓名
                        <asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox>
                    </td>
                    <td>联系电话
                         <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <cc2:btnManager ID="btnSerch" runat="server" OnClick="btnSerch_Click" />
                        <cc2:btnReload ID="btnReload" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>渠道名称</th>
                        <th>渠道类型</th>
                        <th>新人状态</th>
                        <th>邀约负责人</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                            <tr skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#Eval("Channel") %></td>
                                <td><%#GetChannelTypeName(Eval("ChannelType")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetInviteEmployee(Eval("CustomerID")) %></td>
                                <td><a class="btn btn-primary btn-mini Upload btnSaveSubmit<%#Eval("EmployeeID")%> btnSumbmit" href="QuotedPricefileUpload.aspx?CustomerID=<%#Eval("CustomerID") %>&QuotedID=<%#Eval("QuotedID") %>" <%#Eval("Bride")==null?"style='display:none;'":"" %>>上传提案</a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
