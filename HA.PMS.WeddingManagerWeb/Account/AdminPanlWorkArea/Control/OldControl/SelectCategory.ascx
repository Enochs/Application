<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectCategory.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.OldControl.SelectCategory" %>
<div>
    <table style="width: 100%;">
        <tr>
            <td style="width: 1%; vertical-align: top;<%=int.Parse(Request["ParentID"])>0?"display:none;":"" %>" >

                <asp:ListBox ID="lstpg" runat="server" Width="1%" Height="1" AutoPostBack="True" OnSelectedIndexChanged="lstpg_SelectedIndexChanged"></asp:ListBox>

            </td>
            <td>
                <ul>
                    <asp:Repeater ID="repPGlist" runat="server">
                        <ItemTemplate>
                            <li style="list-style:none;">
                                <asp:HiddenField runat="server" ID="hideKey" Value='<%#Eval("QCKey") %>' />
                                <asp:CheckBox ID="chkpg" runat="server" /><%#Eval("Title") %></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </td>


        </tr>
        <tr>
            <td colspan="2">

                <asp:Button ID="btnSaveSelect" runat="server" Text="确认选择" OnClick="btnSaveSelect_Click" CssClass="btn btn-success" />
            </td>

        </tr>
    </table>

</div>
 