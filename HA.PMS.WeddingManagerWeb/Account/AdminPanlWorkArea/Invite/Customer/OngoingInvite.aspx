<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OngoingInvite.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer.OngoingInvite" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="../../Control/SetIntiveSerch.ascx" TagName="SetIntiveSerch" TagPrefix="uc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="uc1" TagName="MessageBoard" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="uc1" TagName="MessageBoardforall" %>
<%@ Register Src="../../Control/MyManager.ascx" TagName="MyManager" TagPrefix="uc2" %>

<%@ Register Src="../../Control/DateRanger.ascx" TagName="DateRanger" TagPrefix="uc3" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script src="/Scripts/trselection.js"></script>

    <script type="text/javascript">
        $(function () {
            $(".DateTimeTxt").hide();
            $("html,body").css({ "background-color": "transparent" });
        });
        function SetDatetime(Control, dates) {
            $(Control).parent().children(".DateTimeTxt").show();
            $(Control).parent().children(".DateTimeTxt").attr("value", dates);
            $(Control).parent().children(".NoneEdit").remove();
        }
        //显示记录沟通记录
        function ShowInviteCommunicationContent(CustomerID, Control) {
            var Url = "/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=" + CustomerID;
            window.location.href = Url;
            //$(Control).attr("id", "updateShow" + CustomerID);
            //showPopuWindows(Url, 850, 600, "a#" + $(Control).attr("id"));
        }
    </script>
    <div class="widget-box" style="height: auto; overflow: auto;">
        <div class="widget-content">
            <table>
                <tr>
                    <td>渠道</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <cc2:ddlChannelType ID="ddlChanneltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChanneltype_SelectedIndexChanged"></cc2:ddlChannelType><cc2:ddlChannelName ID="ddlChannelname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChannelname_SelectedIndexChanged"></cc2:ddlChannelName><cc2:ddlReferee ID="ddlreferrr" runat="server"></cc2:ddlReferee>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>沟通次数
                        <asp:TextBox ID="txtContentCount" MaxLength="3" Width="70" runat="server" ToolTip="沟通次数为整数！" CssClass="{number:true}"></asp:TextBox></td>
                    <td>
                        <uc2:MyManager ID="MyManager" runat="server" />
                    </td>
                    <td>时间:
                        <asp:DropDownList runat="server" ID="ddlDateRanger">
                            <asp:ListItem Text="请选择" Value="-1" />
                            <asp:ListItem Text="婚期" Value="1" />
                            <asp:ListItem Text="录入时间" Value="2" />
                            <asp:ListItem Text="最近沟通时间" Value="3" />
                            <asp:ListItem Text="计划沟通时间" Value="4" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <uc3:DateRanger ID="DateRanger" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">新人姓名
                        <asp:TextBox ID="txtContactMan" runat="server" Width="60px"></asp:TextBox>
                        联系电话
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>

                        <asp:Button ID="btnSerch" runat="server" ClientIDMode="Static" Text="查询" CssClass="btn  btn-primary" OnClick="btnSerch_Click" />

                        <cc2:btnReload ID="BtnReload" runat="server" /></td>
                </tr>
            </table>
        </div>


        <div style="overflow-y: auto;">
            <table class="table table-bordered table-striped" style="border: none;">
                <thead>
                    <tr>
                        <td colspan="9"></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="保存" OnClick="btnSaveDate_Click" CssClass="btn btn-primary" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>分派日期</th>
                        <th>来源渠道</th>
                        <%--<th>推荐人</th>--%>
                        <th>邀约次数</th>
                        <th>上次沟通时间</th>
                        <th>下次沟通时间</th>
                        <th>责任人</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repTelemarketingManager" runat="server">
                        <ItemTemplate>
                            <tr skey='CustomerCustomerID<%#Eval("CustomerID") %>' <%#OverChange(Eval("CustomerID")) %>>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan") %></a></td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("WineShop") %></td>
                                <td><%#ShowShortDate(Eval("CreateDate")) %></td>
                                <td><%#Eval("Channel") %></td>
                                <%--<td><%#Eval("Referee") %></td>--%>
                                <td><%#Eval("FllowCount") %></td>
                                <td><%#GetShortDateString(Eval("LastFollowDate")) %></td>
                                <td>
                                    <label class="NoneEdit"><%#GetShortDateString(Eval("AgainDate")) %></label>
                                    <label onclick="SetDatetime(this,'<%#Eval("AgainDate") %>');" class="NoneEdit">设置沟通时间</label>
                                    <asp:TextBox Style="margin: 0; display: none" ID="txtAgainDate" onclick='WdatePicker({dateFmt:"yyyy/M/d H:mm:ss"});' CssClass="DateTimeTxt" runat="server" Text='<%#Eval("AgainDate") %>' Width="75"></asp:TextBox>
                                    <asp:HiddenField ID="HideKey" runat="server" Value='<%#Eval("InviteID") %>' />
                                    <asp:HiddenField ID="hideOldDate" runat="server" Value='<%#Eval("AgainDate") %>' />
                                </td>
                                <td>
                                    <%#GetEmployeeName(Eval("EmployeeID")) %>
                                </td>
                                <td style="white-space: nowrap;">
                                    <a href="#" class="btn btn-primary  btnSaveSubmit<%#Eval("EmployeeID") %> btnSumbmit" onclick="ShowInviteCommunicationContent(<%#Eval("CustomerID") %>,this);">记录/查看沟通记录</a>
                                    <a target="_blank" class="btn btn-primary " <%=StatuHideViewInviteInfo() %> href="/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1">查看跟踪</a></td>
                                </td>
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
                    <tr>
                        <td colspan="9" runat="server">
                            <asp:Button ID="Button2" runat="server" Text="保存" OnClick="btnSaveDate_Click" CssClass="btn btn-primary" />
                            <asp:Button runat="server" ID="btnExport" Text="导出" OnClick="btnExport_Click" CssClass="btn btn-primary" />
                        </td>
                    </tr>
                </tfoot>
            </table>

            <asp:Repeater runat="server" ID="rptAllManager" OnItemDataBound="rptAllManager_ItemDataBound" Visible="false">
                <ItemTemplate>
                    <table class="table table-bordered table-striped table-select">
                        <thead>
                            <tr>
                                <th>新人</th>
                                <th>联系电话</th>
                                <th>婚期</th>
                                <th>酒店</th>
                                <th>分派日期</th>
                                <th>来源渠道</th>
                                <th>推荐人</th>
                                <th>邀约次数</th>
                                <th>最近一次沟通时间</th>
                                <th>计划再次沟通时间</th>
                                <th>责任人</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="repTelemarketingManager" runat="server">
                                <ItemTemplate>
                                    <tr skey='CustomerCustomerID<%#Eval("CustomerID") %>' <%#OverChange(Eval("CustomerID")) %>>
                                        <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan") %></a></td>
                                        <td><%#Eval("ContactPhone") %></td>
                                        <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                        <td><%#Eval("WineShop") %></td>
                                        <td><%#ShowShortDate(Eval("CreateDate")) %></td>
                                        <td><%#Eval("Channel") %></td>
                                        <td><%#Eval("Referee") %></td>
                                        <td><%#Eval("FllowCount") %></td>
                                        <td><%#GetShortDateString(Eval("LastFollowDate")) %></td>
                                        <td>
                                            <%#Eval("AgainDate") %>
                                        </td>
                                        <td>
                                            <%#GetEmployeeName(Eval("EmployeeID")) %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </ItemTemplate>
            </asp:Repeater>

            <uc1:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
