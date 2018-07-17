<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerTitle.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CustomerTitle" %>
 <table  style="width: 100%;font-size:12px;">
    
        <tr>
            <td style="white-space: nowrap; text-align: right;"><b>订单名称:</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCoder" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpinpai" runat="server" Visible="False"></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>

            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;</td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;</td>

            <td style="white-space: nowrap;"></td>

            <td style="white-space: nowrap;"></td>
        </tr>
        <tr>
            <td style="white-space: nowrap; text-align: right;"><b>新人:</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>联系方式:</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>酒店:</b></td>
            <td style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblHotel" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>婚期:</b></td>
            <td style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPartyDate" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;">
                <b>时段:</b>
            </td>
            <td style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTimerSpan" runat="server" Text=""></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="white-space: nowrap; text-align: right;"><b>策划师：</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEmpLoyee" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>联系方式:</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCellPhone" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>
            <td style="text-align: left;">&nbsp;</td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>
            <td style="text-align: left;">&nbsp;</td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>
            <td style="text-align: left;">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
            <tr>
            <td style="white-space: nowrap; text-align: right;"><b>订单类型:</b></td>
            <td style="white-space: nowrap; text-align: left;">
                <asp:DropDownList ID="DropDownList1" runat="server" Width="85">
                </asp:DropDownList>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>风格:</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DropDownList2" runat="server" Width="85">
                </asp:DropDownList>
            </td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>

            <td style="white-space: nowrap; text-align: left;"><b>套系名称:</b></td>
            <td style="white-space: nowrap; text-align: right;">
                <asp:DropDownList ID="DropDownList3" runat="server" Width="85">
                </asp:DropDownList>
            </td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;</td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;</td>

            <td style="white-space: nowrap;">&nbsp;</td>

            <td style="white-space: nowrap;">&nbsp;</td>
        </tr>
    </table>