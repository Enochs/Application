<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="Sys_ControlsCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_ControlsCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>创建控件
        </legend>

        <table width="371" height="179" border="1">
            <tr>
                <td width="96">功能名称</td>
                <td width="238">
                    <asp:TextBox ID="txtControlTitle"  CssClass="{required:true}"  runat="server" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="96">控件Key</td>
                <td width="238">
                    <asp:TextBox ID="txtControlKey" CssClass="{required:true}"   runat="server" MaxLength="32"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    控件位置
                </td>
                <td>
                    <asp:RadioButtonList ID="rdolistType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">页面</asp:ListItem>
                        <asp:ListItem Value="2">循环</asp:ListItem>
                    </asp:RadioButtonList>


                </td>
            </tr>
             
            <tr>
                <td>
                    CSS名称</td>
                <td>
                    <asp:TextBox ID="txtCssClass"  CssClass="{required:true}"  runat="server" MaxLength="128"></asp:TextBox>

                </td>
            </tr>
             
            <tr>
                <td>
                    ServerType</td>
                <td>
                    <asp:DropDownList ID="ddlServerType" runat="server">
                        <asp:ListItem Value="0" Selected="True">服务器</asp:ListItem>
                        <asp:ListItem Value="1">客户端</asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>
             
            <tr>
                <td>
                    <asp:Button ID="btnControls" runat="server" Text="确认" OnClick="btnControls_Click" /></td>
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
