<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceEcport.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceEcport" Title="打印或者展示" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>


<%@ Register Src="../Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc1" %>


<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <div id="PrintNode" style="border:groove;">
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <div style="text-align: center">
                        <asp:Label ID="lblTop" runat="server" Text=""></asp:Label>
                    </div>
                    <uc1:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />
                    <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound">
                        <ItemTemplate>
                            <table class=" First<%#Eval("CategoryID") %>" border="1" style="border: solid 1px #808080; width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 200px; white-space: nowrap;"><b>类别</b></td>
                                    <td style="width: 240px; white-space: nowrap;"><b>项目</b></td>
                                    <td style="width: 150px; white-space: nowrap;"><b>产品/服务内容</b></td>
                                    <td style="width: 150px; white-space: nowrap;"><b>具体要求</b></td>
                                    <td style="width: 75px; white-space: nowrap;"><b>单价</b></td>
                                    <td style="width: 100px; white-space: nowrap;"><b>单位</b></td>
                                    <td style="width: 100px; white-space: nowrap;"><b>数量</b></td>
                                    <td style="width: 150px; white-space: nowrap;" colspan="3"><b>小计</b></td>
                                    <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                                </tr>
                                <asp:Repeater ID="repdatalist" runat="server">
                                    <ItemTemplate>
                                        <tr style="height: 13px;">
                                            <td <%#Container.ItemIndex>0?"class='NeedHide'  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" : "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:nowrap;'"%> <%#Eval("ParentCategoryID")%>>
                                                <b <%#Container.ItemIndex>0?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b><br />
                                            </td>
                                            <td <%#Eval("ItemLevel").ToString()=="3"?"style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'": "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:nowrap;'" %>>
                                                <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b><br />
                                            </td>
                                            <td>
                                                <%#GetProductByID(Eval("ProductID"))%>
                                            </td>
                                            <td>
                                                <%#Eval("Requirement") %></td>

                                            <td class="SetSubtotal<%#Eval("ProductID") %>">
                                                <%#Eval("UnitPrice") %></td>
                                            <td>
                                                <%#Eval("Unit") %></td>
                                            <td class="SetSubtotal<%#Eval("ProductID") %>">
                                                <%#Eval("Quantity") %></td>
                                            <td class="SetSubtotal<%#Eval("ProductID") %> Total<%#Eval("ParentCategoryID") %>" colspan="3">
                                                <%#Eval("Subtotal") %></td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div style="text-align: right; margin-right: 368px;">
                                分项合计:
                <%#Eval("ItemAmount")%>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </td>
            </tr>
            <tr>
                <td>
                    <table border="1" style="border: solid 1px #808080; width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 200px; white-space: nowrap;"><b>总金额</b></td>

                            <td>
                                <asp:Label ID="txtAggregateAmount" runat="server" ClientIDMode="Static" Enabled="false"></asp:Label></td>
                            <td>定金</td>
                            <td>
                                <asp:Label ID="txtRealAmount" runat="server" ClientIDMode="Static"></asp:Label>
                            </td>
                            <td>预付款
                            </td>
                            <td colspan="4">
                                <asp:Label ID="txtEarnestMoney" runat="server" ClientIDMode="Static"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>说明</td>
                            <td colspan="9">

                                <asp:Label ID="lblNode" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>
        <table border="1" style="width: 100%;" cellpadding="0" cellspacing="0">

            <tr>
                <td colspan="10">
                    <asp:Label ID="lblButton" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
