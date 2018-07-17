<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sys_ChannelUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Jurisdiction.Sys_ChannelUpdate" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            height: 18px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "500px", "height": "500px", "background-color": "transparent" });
        });
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSaveDate.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtChannelName.ClientID%>');
             BindInt('<%=txtOrderCode.ClientID%>');
            BindString(50, '<%=txtClass.ClientID%>:<%=txtgettype.ClientID%>');
             BindString(100, '<%=txtChannelAddress.ClientID%>');
         }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
    <div style="height: 600px;">
        <table style="width: 100%;">

            <tr>
                <td>添加频道</td>
                <td></td>
            </tr>

            <tr>
                <td>频道名称</td>
                <td>
                    <asp:TextBox ID="txtChannelName" check="1" tip="限20个字符！" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox>
                    <span style="color: red">*</span>
                </td>
            </tr>

            <tr>
                <td class="auto-style1">频道地址</td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtChannelAddress" check="1" tip="限100个字符！" CssClass="{required:true}" runat="server" MaxLength="100"></asp:TextBox>
                    <span style="color: red">*</span>
                </td>
            </tr>

            <tr>
                <td>类型名</td>
                <td>
                    <asp:TextBox ID="txtgettype" check="1" tip="限50个字符！" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox>
                    <span style="color: red">*</span>
                </td>
            </tr>

            <tr>
                <td>样式</td>
                <td>
                    <asp:TextBox ID="txtClass" check="0" tip="限50个字符！" CssClass="{required:true}" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>是否为菜单</td>
                <td>
                    <asp:CheckBox ID="chkismenu" runat="server" Text="是否为菜单" />
                </td>
            </tr>

            <tr>
                <td>上级节点</td>
                <td>
                    <asp:DropDownList ID="ddlNode" runat="server" Width="170px">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>排序编号</td>
                <td>
                    <asp:TextBox ID="txtOrderCode" check="0" tip="只能为整数，不填默认为 0" runat="server" MaxLength="10"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSaveDate" runat="server" Text="保存" OnClick="btnSaveDate_Click" CssClass="btn btn-success" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


