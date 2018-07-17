<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyManager.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.MyManager" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<table>
    <tr>
        <td style="width: 40px; white-space: nowrap; vertical-align: bottom; text-align: center;">
            <div style="margin-bottom: 8px;"><%=Title %></div>
        </td>
        <td>

            <cc1:ddlMyManagerEmployee ID="ddlMyManagerEmployee1" Height="27" Width="120" runat="server">
            </cc1:ddlMyManagerEmployee>

        </td>
    </tr>
</table>


