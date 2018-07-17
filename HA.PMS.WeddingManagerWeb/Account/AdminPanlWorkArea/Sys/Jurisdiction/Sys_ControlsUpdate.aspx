<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="Sys_ControlsUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_ControlsUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>

        <legend>修改控件界面</legend>
        <table width="381" height="124" border="1">
            <tr>
                <td width="88">控件Key</td>
                <td width="246">
                    <asp:TextBox ID="txtControlKey" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox></td>
            </tr>
            <tr>
                <td>控件位置</td>
                <td>
                    <asp:TextBox ID="txtBelongByControl" CssClass="{required:true}" runat="server" MaxLength="32"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnUpdate" runat="server" Text="确认" OnClick="btnUpdate_Click" /></td>
                <td></td>
            </tr>
        </table>
    </fieldset>
     <script type="text/javascript">

         $(document).ready(function () {
             $("html,body").css({ "width": "500px", "height": "500px", "background-color": "transparent" });
         });

     </script>
</asp:Content>
