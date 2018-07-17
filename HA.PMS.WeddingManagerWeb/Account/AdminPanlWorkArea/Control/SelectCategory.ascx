<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectCategory.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SelectCategory" %>
<div>

    <table style="width: 90%;" id="SaleProduct">
        <tr>
            <td style="vertical-align:top;">

                <asp:Repeater ID="repPGlist" runat="server" OnItemCommand="repPGlist_ItemCommand">
                    <ItemTemplate>
                        <li style="list-style: none;">
                            <asp:HiddenField runat="server" ID="hideKey" Value='<%#Eval("QCKey") %>' />
                            <asp:CheckBox ID="chkpg" runat="server" /><asp:LinkButton ID="lnkbtnSelect" runat="server" CommandArgument='<%#Eval("QCKey") %>'><%#Eval("Title") %></asp:LinkButton>
                            </li>
                    </ItemTemplate>
                </asp:Repeater>

            </td>
            <td style="vertical-align: top; text-align: left;">

                <table style="width:100%;" border="1" id="TablerepProductByCatogryforWarehouseList0">

                    <tr>

                        <td>产品</td>

                        <td>价格</td>


                    </tr>
                    <asp:Repeater ID="repSaleProduct" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("ProductName") %></td>

                                <td><%#Eval("SalePrice") %></td>


                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                </table>

            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:HiddenField ID="hideSelectProduct" runat="server" ClientIDMode="Static" />
                <asp:Button ID="btnSaveSelect" runat="server" Text="确认选择" OnClick="btnSaveSelect_Click" CssClass="btn btn-success" />
            </td>
        </tr>
    </table>
</div>
