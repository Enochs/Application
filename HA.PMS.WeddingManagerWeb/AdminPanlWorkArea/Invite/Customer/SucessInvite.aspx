<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SucessInvite.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer.SucessInvite" Title="邀约成功" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="uc1" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="uc1" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="uc1" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="uc1" TagName="DateRanger" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <style type="text/css">
        .table thead tr th {
            width: auto;
        }
    </style>
    <script type="text/javascript">
        //function ShowInviteCommunicationContent(CustomerID, Control) {
        //    var Url = "/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=" + CustomerID + "&Sucess=1";
        //    $(Control).attr("id", "updateShow" + CustomerID);
        //    showPopuWindows(Url, 800, 600, "a#" + $(Control).attr("id"));
        //}
        $(document).ready(function () {
            $("html").css({ "overflow-x": "hidden" });
        });

        //显示记录沟通记录
        function ShowInviteCommunicationContent(CustomerID, Control) {
            var Url = "/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=" + CustomerID + "&&OnlyView=1";
            window.location.href = Url;
        }

    </script>
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>
                    <td>渠道类型<cc2:ddlChannelType ID="ddlChanneltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChanneltype_SelectedIndexChanged"></cc2:ddlChannelType></td>
                    <td>渠道名称<cc2:ddlChannelName ID="ddlChannelname" runat="server" AutoPostBack="false" Width="120px"></cc2:ddlChannelName></td>
                    <td>时间<asp:DropDownList ID="ddlType" Width="90px" runat="server">
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="0">邀约成功</asp:ListItem>
                        <asp:ListItem Value="1">婚期</asp:ListItem>
                        <asp:ListItem Value="2">到店时间</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        <uc1:DateRanger runat="server" ID="DateRanger" />
                    </td>
                    <td colspan="2">用户类型:
                            <asp:DropDownList runat="server" ID="ddlEmployeeType">
                                <asp:ListItem Text="请选择" Value="-1" />
                                <asp:ListItem Text="录入人" Value="Recorder" />
                                <asp:ListItem Text="电销" Value="InviteEmployee" />
                                <asp:ListItem Text="婚礼顾问" Value="ToEmployee" />
                            </asp:DropDownList></td>
                    <td>
                        <uc1:MyManager runat="server" ID="MyManager" Title="" />
                    </td>
                </tr>
                <tr>
                    <td>新人姓名
                        <asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox>
                    </td>
                    <td>联系电话
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSerch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnSerch_Click" />
                        <cc2:btnReload ID="btnReload2" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th style="white-space: nowrap;">新人姓名</th>
                        <th style="white-space: nowrap;">联系电话</th>
                        <th style="white-space: nowrap;">婚期</th>
                        <th style="white-space: nowrap;">酒店</th>

                        <th style="white-space: nowrap;">渠道名称</th>
                        <th style="white-space: nowrap;">预计到店时间</th>
                        <th style="white-space: nowrap;">实际到店时间</th>
                        <th style="white-space: nowrap;">录入人</th>
                        <th style="white-space: nowrap;">电销</th>
                        <th style="white-space: nowrap;">婚礼顾问</th>
                        <th style="white-space: nowrap;">沟通记录</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repTelemarketingManager" runat="server" OnItemDataBound="repTelemarketingManager_ItemDataBound">
                        <ItemTemplate>
                            <tr>

                                <td><a target="_blank" href="/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan") %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("WineShop") %></td>

                                <td><%#Eval("Channel") %></td>
                                <td><%#GetPlanComeDateByCustomerID(Eval("CustomerID")) %></td>
                                <td><%#Eval("OrderCreateDate") %></td>
                                <td><%#GetEmployeeName(Eval("Recorder")) %></td>
                                <td><%#GetEmployeeName(Eval("InviteEmployee")) %></td>
                                <td><%#GetEmployeeName(Eval("ToEmployee")) %></td>
                                <%--<td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>--%>
                                <td>
                                    <%--<a href="#" class="btn btn-primary" onclick="ShowInviteCommunicationContent(<%#Eval("CustomerID") %>,this);">记录/查看</a>--%>
                                    <a target="_blank" class="btn btn-primary " <%=StatuHideViewInviteInfo() %> href="/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1">查看跟踪</a></td>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="10" style="text-align: left;">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" PageSize="20" AlwaysShow="true" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <uc1:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
