<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherallCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Other.OtherallCreate" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AddContent(CustomerID) {
            var Url = "/AdminPanlWorkArea/CS/Member/Anniversary/AnniversaryCreate.aspx?CustomerID=" + CustomerID + "&Type=3";
            showPopuWindows(Url, 765, 200, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
        $(document).ready(function () {
            $("#checkall").click(function () {
                if ($(this)[0].checked) {
                    $(".subcheckbox").each(function () { $(this)[0].checked = true; });
                }
                else {
                    $(".subcheckbox").each(function () { $(this)[0].checked = false; });
                }
            });
            $(".subcheckbox").click(function () {
                if (($(".subcheckbox:checked").length) == $(".subcheckbox").length) {
                    $("#checkall")[0].checked = true;
                }
                else { $("#checkall")[0].checked = false; }
            });
        });
        function ShowSendMessAge(TypeID) {
            var Url = "/AdminPanlWorkArea/CS/Member/SendMessage.aspx?TypeID=" + TypeID + "&CustomerID=" + getCustomersIDs();
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis")
            $("#SelectEmpLoyeeBythis").click(); 
        }
        function getCustomersIDs()
        {
            var _IDs = "";
            $(".subcheckbox").each(function () {
                if ($(this)[0].checked) {
                    _IDs = _IDs + $(this).parent("td").find("input[type=hidden]").val() + ",";
                }
            });
            return _IDs.substring(0, _IDs.length - 1);
       }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;"></a>
        <div class="widget-box">
            <div class="widget-box" style="height: 60px; border: 0px;">
                <table class="queryTable" style="border-bottom: 1px solid #c7d5de;">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>新人姓名：<asp:TextBox ID="txtBride" runat="server" MaxLength="10"></asp:TextBox></td>
                                    <td>联系电话：<asp:TextBox ID="txtBrideCellPhone" runat="server" MaxLength="20"></asp:TextBox></td>
                                    <td>时间：<asp:DropDownList ID="ddlDateType" Width="80" runat="server">
                                         <asp:ListItem Text="" Value="0"/>
                                        <asp:ListItem Text="婚期" Value="1" />
                                        <asp:ListItem Text="生日" Value="2" />
                                    </asp:DropDownList>
                                    </td>
                                    <td><ha:dateranger runat="server" ID="QueryDateRanger" /></td>
                                    <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                                    <td><asp:Button ID="BtnQuery" Height="27" OnClick="BinderData" CssClass="btn btn-primary" runat="server" Text="查询" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td><input id="btnSendMessage" class="btn btn-primary" type="button" value="发送短信" onclick="ShowSendMessAge(6);" /></td>
                    </tr>
                </table>
            </div>
            <br />
            <table class="table table-bordered table-striped">
                <thead>
                    <tr id="trContent">
                        <th><input type="checkbox" id="checkall"/></th>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>生日</th>
                        <th>服务方式</th>
                        <th>补充说明</th>
                        <th>计划完成时间</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound" OnItemCommand="repCustomer_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><input type="checkbox" class="subcheckbox" /><input type="hidden" value='<%#Eval("CustomerID") %>'/></td>
                                <td style="word-break: break-all;"><a target="_blank" href="..\CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID")%>"><%#Eval("Bride") %></a></td>
                                <td style="word-break: break-all;"><%#Eval("BrideCellPhone") %></td>
                                <td style="word-break: break-all;"><%#ShowShortDate(Eval("PartyDate")) %></td>
                                <td style="word-break: break-all;"><%#Eval("Wineshop") %></td>
                                <td style="word-break: break-all;"><%#GetOrderEmpLoyeeName(Eval("OrderID")) %></td>
                                <td style="word-break: break-all;"><%#GetQuotedEmpLoyeeName(Eval("OrderID")) %></td>
                                <td style="word-break: break-all;"><%#ShowShortDate(Eval("BrideBirthday")) %></td>
                                <td><asp:DropDownList ID="ddlType" runat="server" Width="75"></asp:DropDownList></td>
                                <td><asp:TextBox ID="txtRemark" MaxLength="50" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtFinishDate" onclick="WdatePicker()" runat="server"></asp:TextBox></td>
                                <td><asp:LinkButton ID="btnSaveChange" CssClass="btn btn-mini btn-success" runat="server" CommandArgument='<%#Eval("CustomerID")%>'>保存数据</asp:LinkButton></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="CtrPageIndex" PageSize="10" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
</asp:Content>
