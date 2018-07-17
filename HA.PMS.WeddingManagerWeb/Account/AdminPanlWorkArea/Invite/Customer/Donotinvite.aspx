<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Donotinvite.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer.Donotinvite" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="../../Control/SetIntiveSerch.ascx" TagName="SetIntiveSerch" TagPrefix="uc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="uc1" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="uc1" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="uc1" TagName="MyManager" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="uc1" TagName="CstmNameSelector" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <script src="/Scripts/trselection.js"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="widget-box">
        <div class="widget-content">
            <a id="createFD_Telemarketing" href="FD_TelemarketingCreate.aspx" style="display: none;">添加新人信息录入</a>
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
                    <td>
                        <uc1:MyManager runat="server" ID="MyManager" />
                    </td>
                    <td><%--新人姓名
                        <asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox>--%>
                        <uc1:CstmNameSelector runat="server" ID="CstmNameSelector" Title="新人姓名" />
                    </td>
                    <td>联系电话
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                    </td>
                    <td>

                        <cc2:btnManager ID="btnSerch" runat="server" OnClick="btnSerch_Click" /></td>
                    <td>

                        <cc2:btnReload ID="btnReload2" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <td colspan="9"></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>来源渠道</th>
                        <th>推荐人</th>
                        <th>录入时间</th>
                        <th>录入人</th>
                        <th>邀约人</th>
                        <th>说明</th>
                        <th>邀约</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repTelemarketingManager" runat="server" OnItemCommand="repTelemarketingManager_ItemCommand">
                        <ItemTemplate>
                            <tr skey='CustomerCustomerID<%#Eval("CustomerID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan") %></a></td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("WineShop") %></td>
                                <td><%#Eval("Channel") %></td>
                                <td><%#Eval("Referee") %></td>
                                <td><%#ShowShortDate(Eval("CreateDate")) %></td>
                                <td><%#GetEmployeeName(Eval("CreateEmpLoyee")) %></td>
                                <td><%#GetEmployeeName(Eval("InviteEmployee")) %></td>
                                <td>
                                    <lable style="cursor: default" title='<%#Eval("Other") %>'><%#ToInLine(Eval("Other")) %></lable>
                                </td>
                                <td>
                                    <asp:Button ID="btnSaveRow" runat="server" Text="填写跟踪记录" CommandArgument='<%#Eval("CustomerID") %>' CommandName="SaveRow" CssClass="btn btn-primary" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9" style="text-align: left;">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </tfoot>
            </table>
            <uc1:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>

