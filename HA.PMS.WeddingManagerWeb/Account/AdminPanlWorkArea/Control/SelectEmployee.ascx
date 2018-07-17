<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectEmployee.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SelectEmployee" %>


<table width="98%" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
    <tr>
        <td>
            <table width="100%" border="0" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF">
                <tr>
                    <td>
                        <asp:TreeView ID="treeDepartment" runat="server" Width="65%" OnSelectedNodeChanged="treeDepartment_SelectedNodeChanged"></asp:TreeView>
                    </td>
                    <td>

                        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#CCCCCC">
                            <tr>
                                <td>


                                    <table width="100%" border="0" cellpadding="5" cellspacing="1" bgcolor="#FFFFEE">
                                        <asp:Repeater ID="repEmpLoyeeList" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <input id="rdoEmpLoyee" type="radio" value='<%#Eval("EmployeeID") %>' runat="server" /></td>
                                                    <td><%#Eval("EmployeeName") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSaveSelect" runat="server" Text="确认选择" OnClick="btnSaveSelect_Click" Style="width: 78px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

 