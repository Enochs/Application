<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost.Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/CustomerTitle.ascx" TagPrefix="HA" TagName="CustomerTitle" %>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <table class="table table-bordered table-striped" style="width: 100%;">
            <thead>
                <tr>
                    <td colspan="4">
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btn_Updates" Text="修改" OnClick="btn_Updates_Click" />
                    </td>
                </tr>
                <tr style="background-color: aliceblue;">
                    <td>合同金额:<asp:Label ID="lblSaleAmount" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>已付款:<asp:Label ID="lblFinishMoney" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td colspan="2">余款:<asp:Label ID="lblHaveMoney" runat="server" Text="Label"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <th>收款时间</th>
                    <th>收款金额</th>
                    <th>付款方式</th>
                    <th>收款理由</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repQuotedPlan" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="height: 16px;"><%#GetShortDateString(Eval("CollectionTime"))%>
                                <asp:HiddenField runat="server" ID="HidePlanId" Value='<%#Eval("PlanID") %>' />
                            </td>
                            <td style="height: 16px;">
                                <asp:TextBox runat="server" ID="txtRealityAmount" Text='<%#Eval("RealityAmount") %>' />
                            </td>
                            <td style="height: 16px;">
                                <%--<asp:RadioButtonList ID="rdoMoneytypes" runat="server" RepeatDirection="Horizontal" Width="100%">
                                    <asp:ListItem Text="现金" Value="1">现金</asp:ListItem>
                                    <asp:ListItem Text="刷卡" Value="2">刷卡</asp:ListItem>
                                    <asp:ListItem Text="转账" Value="3">转账</asp:ListItem>
                                </asp:RadioButtonList>--%>
                            </td>
                            <td style="height: 16px;">
                                <asp:TextBox runat="server" ID="txtNode" Text='<%#Eval("Node") %>' TextMode="MultiLine" Width="281px" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btn_del" CommandName="Del" Text="删除" OnClientClick="return confirm('你确定要删除吗?');" CssClass="btn btn-danger btn-mini" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

    </div>
</asp:Content>
