<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CS_TakeforNoneNotice.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_TakeforNoneNotice" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <%--        <a href="CS_TakeDiskCreate.aspx" class="btn btn-primary  btn-mini" id="createTakeDisk">创建取件</a>--%>
        <div class="widget-box">


            <div class="widget-box" style=" border: 0px;">

                <table class="queryTable">
                    <tr>
                        <td>
                            <table>
                                <tr>

                                    <td>新人姓名:<asp:TextBox ID="txtGroom" runat="server"></asp:TextBox>
                                    </td>
                                    <td>电话:<asp:TextBox ID="txtGroomCellPhone" runat="server"></asp:TextBox>
                                    </td>
                                    <td>婚期:<asp:TextBox ID="txtStart" onclick="WdatePicker();" runat="server"></asp:TextBox>
                                    </td>
                                    <td>至<asp:TextBox ID="txtEnd" onclick="WdatePicker();" runat="server"></asp:TextBox>
                                    </td>
                                    <td>酒店:<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <HA:MyManager runat="server" ID="MyManager" Title="策划师" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnQuery" CssClass="btn btn-primary" Height="27" runat="server" Text="查询" OnClick="btnQuery_Click" />
                                        <cc2:btnReload ID="btnReload2" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </div>

            <table class="table table-bordered table-striped">
                <thead>
                    <tr id="trContent">
                        <th>新人姓名</th>
                        <th>电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>

                        <th>新人取件时间</th>
                        <th>通知新人时间</th>
                        <th>备注</th>
                        <th>操作</th>
                    </tr>
                </thead>

                <tbody id="tbContent">
                    <asp:Repeater ID="rptTalkeDisk" runat="server" OnItemCommand="rptTalkeDisk_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><a href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1" target="_blank"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("GroomCellPhone") %></td>
                                <td><%#ShowPartyDate( Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeName(GetOrderIdByCustomerID(Eval("CustomerID"))) %></td>
                                <td><%#GetQuotedEmpLoyeeName(GetOrderIdByCustomerID(Eval("CustomerID"))) %></td>
                                <td>
                                    <asp:TextBox ID="txtRealityTime" Width="85" runat="server" onclick="WdatePicker();" Text='<%#Eval("realityTime","{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoticeTime" Width="85" runat="server" onclick="WdatePicker();" Text='<%#Eval("NoticeTime","{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtRemark" Text='<%#Eval("Remark") %>' /></td>
                                <td>
                                    <asp:LinkButton ID="lkbtnSave" Text="保存" CssClass="btn btn-primary  btn-mini" runat="server" CommandArgument='<%#Eval("TakeID") %>' CommandName="Edit"></asp:LinkButton>
                                    <a href="CS_TakeDiskCheck.aspx?Type=Look&TakeID=<%#Eval("TakeID") %>" class="btn btn-primary btn-mini">审核记录</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="TalkeDiskPager" OnPageChanged="TalkeDiskPager_PageChanged" runat="server"></cc1:AspNetPagerTool>

        </div>
    </div>
</asp:Content>

