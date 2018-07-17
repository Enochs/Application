<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Customer.CustomerManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>



<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
    <script>function SubmitDelete(ctrl) { return confirm("确认要永久删除新人“" + $(ctrl).next("input[type=hidden]").val() + "”的所有信息吗？删除后将无法恢复！") == true ? confirm("真的要删除吗？") : false; }</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div class="widget-box" style="height: 30px; border: 0px;">
        <table class="queryTable">
            <tr>
                <%--<td>新人姓名：<asp:TextBox ID="txtBride" runat="server" /></td>--%>
                <td><HA:CstmNameSelector runat="server" ID="CstmNameSelector" /></td>
                <td><HA:DateRanger runat="server" Title="婚期：" ID="PartyDateRanger" /></td>
                <td>
                    联系电话:
                    <asp:TextBox runat="server" ID="txtCellPhone" />
                </td>
                <td style="display:none">新人状态：<cc2:ddlCustomersState ID="DdlCustomersState1" runat="server"></cc2:ddlCustomersState></td>
                <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                <td><asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="BinderData" runat="server" Text="查询" />
                     <cc2:btnReload ID="btnReload2" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <table class="table table-bordered table-striped">
        <thead>
            <tr>
                <th>新人姓名</th>
                <th>联系电话</th>
                <th>婚期</th>
                <th>酒店</th>
                <th>操作</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repTelemarketingManager" runat="server" OnItemCommand="repTelemarketingManager_ItemCommand">
                <ItemTemplate>
                    <tr skey='Customer<%#Eval("CustomerID") %>'>
 <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan") %></a></td>
                            <td><%#Eval("ContactPhone") %></td>
                        <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                        <td><%#Eval("WineShop") %></td>
                        <td><asp:LinkButton ID="lnkbtnDelete" CssClass="btn btn-danger btn-mini" CommandName="Delete" CommandArgument='<%#Eval("CustomerID") %>' runat="server" OnClientClick='return SubmitDelete(this);'>删除</asp:LinkButton>
                            <asp:HiddenField ID="hidemsg" Value='<%#ShowCstmName(Eval("Bride"),Eval("Groom"))%>' runat="server" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr><td colspan="5" style="text-align: left;"><cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BinderData"></cc1:AspNetPagerTool></td></tr>
        </tfoot>
    </table>
</asp:Content>
