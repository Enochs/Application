<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskCustomerTitle.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CarrytaskCustomerTitle" %>
 <table  class="table table-bordered table-striped"  style="width: 100%;font-size:12px;">
        <tr>
            <td style="white-space: nowrap; text-align: left;" colspan="6"><b>订单编号:</b><asp:Label ID="lblCoder" runat="server" Text=""></asp:Label>
            </td>
            
        </tr>
        <tr>
            <td style="white-space: nowrap; text-align: left;"><b>新娘:</b></td>
            <td style="white-space: nowrap; text-align: center;"><asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>联系方式:</b></td>
            <td style="white-space: nowrap; text-align: center;"><asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>婚期:</b></td>
            <td style="white-space: nowrap; text-align: center;"><asp:Label ID="lblPartyDate" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblTimerSpan" runat="server" Text=""></asp:Label>
            </td>
            
        </tr>
        <tr>
            <td style="white-space: nowrap; text-align: left;"><b>新郎:</b></td>
            <td style="white-space: nowrap; text-align: center;"><asp:Label ID="lblGroomname" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;"><strong>联系方式:</strong></td>
            <td style="white-space: nowrap; text-align: center;"><asp:Label ID="lblGroomPhone" runat="server" Text=""></asp:Label>
            </td>
            
            <td style="white-space: nowrap; text-align: right;"><b>酒店:</b></td>
            
            <td style="white-space: nowrap; text-align: center;"><asp:Label ID="lblHotel" runat="server" Text=""></asp:Label>
            </td>
            
        </tr>
        <tr style="display:none">
            <td style="white-space: nowrap; text-align: left;">&nbsp;</td>
            <td style="white-space: nowrap; text-align: center;">&nbsp;</td>
            <td style="white-space: nowrap; text-align: right;">
                </td>
            <td style="white-space: nowrap; text-align: center;">
            </td>
            <td style="white-space: nowrap; text-align: right;"><strong style="display:none;">余款:</strong></td>
            <td style="white-space: nowrap; text-align: center;">
                <asp:Label ID="lblyukuan" runat="server" Text="" Visible="false"></asp:Label>
            </td>
            
        </tr>
    </table>
<br />