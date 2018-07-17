<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_SaleSourcesCreateManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SaleSourcesCreateManager" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>

<%@ Register Src="../../Control/DateRanger.ascx" TagName="DateRanger" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").attr({ "scroll": "no" }).css({ "background-color": "transparent" });
            BindString(2, 6, "<%=txtGroom.ClientID%>");
            BindQQ("<%=txtQQ.ClientID%>");
            BindMobile("<%=txtGroomCellPhone.ClientID%>");
            BindDate("<%=txtPartyDate.ClientID%>");
            BindText(200, "<%=txtOther.ClientID%>");
            BindCtrlEvent('input[check],textarea[check]');
        });
        function checksave(ctrl) {
            if (ValidateForm('input[check],textarea[check]')) {
                $(ctrl).hide();
                return true;
            }
            return false;
        }
        function ShowUpdateWindows(CustomerID) {;
            var Url = "/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SaleSourcesCustomerUpdate.aspx?CustomerID=" + CustomerID;
            showPopuWindows(Url, "600", "400", "a#acallback");
            $("#acallback").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" id="isValidate" name="isValidate" value="" />
    <a href="#" style="display: none" id="acallback"></a>
    <div class="widget-box" style="width: 99%">
        <div class="widget-content">
            <table>
                <tr>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager1" />
                    </td>
                    <td>
                        <uc1:DateRanger ID="DateRanger" runat="server" Title="录入时间" />
                    </td>
                    <td>
                        <cc2:btnManager ID="btnSerch" runat="server" OnClick="btnSerch_Click" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>渠道类型</th>
                        <th>渠道名称</th>
                        <th>推荐人</th>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>联系QQ</th>
                        <th>婚期</th>
                        <th>时段</th>
                        <th>酒店</th>
                        <th>说明</th>
                        <th>录入时间</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:DropDownList Style="margin: 0; width: 64px" ID="ddlChannelType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChannelType_SelectedIndexChanged"></asp:DropDownList></td>
                        <td>
                            <cc2:ddlChannelName Style="margin: 0; width: 128px" ID="ddlChannelName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChannelName_SelectedIndexChanged"></cc2:ddlChannelName></td>
                        <td>
                            <cc2:ddlReferee Style="margin: 0; width: 64px" ID="DdlReferee1" runat="server"></cc2:ddlReferee></td>
                        <td>
                            <asp:TextBox Style="margin: 0; width: 64px" tip="（必填）限2~6个字符" check="1" Width="50" MaxLength="16" CssClass="Chectxt" ID="txtGroom" runat="server"></asp:TextBox></td>
                        <td>
                            <asp:TextBox Style="margin: 0;" Width="90" CssClass="Chectxt" check="1" tip="（必填）手机号码为11位数字" ID="txtGroomCellPhone" runat="server" MaxLength="20"></asp:TextBox></td>
                        <td>
                            <asp:TextBox Style="margin: 0;" ID="txtQQ" check="0" tip="QQ号码为5~11个位数字" runat="server" Width="90" MaxLength="16"></asp:TextBox></td>
                        <td>
                            <asp:TextBox Style="margin: 0;" Width="70" check="0" ID="txtPartyDate" onclick="WdatePicker();" tip="举办婚礼日期" runat="server" MaxLength="20"></asp:TextBox></td>
                        <td>
                            <asp:DropDownList Style="margin: 0;" ID="ddlTimerSpan" ToolTip="时段" runat="server" Width="64">
                                <asp:ListItem Value="0" Selected="True">中午</asp:ListItem>
                                <asp:ListItem Value="1">晚上</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <cc2:ddlHotel Style="margin: 0;" ID="ddlHotel" ToolTip="酒店" Width="95" runat="server"></cc2:ddlHotel></td>
                        <td>
                            <asp:TextBox Style="margin: 0;" ID="txtOther" check="0" tip="限200个字符" runat="server" Width="90" MaxLength="200"></asp:TextBox></td>
                        <td>
                            <asp:Button Style="height: 21px; margin-left: 0; margin-right: 0; margin-top: 0;" ID="btnAddCustomer" CssClass="btn btn-success btnSave" ToolTip="保存新人" OnClientClick="return checksave(this);" runat="server" Text="保存" OnClick="btnAddCustomer_Click" UseSubmitBehavior="true" /></td>
                    </tr>
                    <asp:Repeater ID="rptCustomer" runat="server">
                        <ItemTemplate>
                            <tr id='FD_SaleSourcesCustomerID<%#Eval("CustomerID") %>'>
                                <td>
                                    <label style="cursor: default" title='<%#Eval("ChannelType") %>'><%#ToInLine(GetChannelTypeName(Eval("ChannelType")),5) %></label></td>
                                <td><a target="_blank" href="<%#Eval("Channel")=="自己收集"?"":"/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SaleSourcesDetails.aspx?SourceID="+GetChannelIDByName(Eval("Channel")) %>" title='<%#Eval("Channel") %>'><%#ToInLine(Eval("Channel")) %></a></td>
                                <td><%#ToInLine(Eval("Referee"),5) %></td>
                                <td><a href="/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#Eval("Bride") %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#Eval("BrideQQ")=="0"?string.Empty:Eval("BrideQQ") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("TimeSpans") %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td>
                                    <lable style="cursor: default" title='<%#Eval("Other") %>'><%#ToInLine(Eval("Other")) %></lable>
                                </td>
                                <td><%#Eval("CreateDate") %></td>
                                <td><a onclick="ShowUpdateWindows('<%#Eval("CustomerID") %>')" class="btn btn-primary btn-mini">修改</a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="11">
                            <cc1:AspNetPagerTool ID="CustomersPager" AlwaysShow="true" PageSize="10" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
