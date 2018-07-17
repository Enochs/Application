<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sys_ChannelCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Jurisdiction.Sys_ChannelCreate" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

 <asp:Content ID="Content2" runat="server" contentplaceholderid="head">
     <script type="text/javascript">
         $(document).ready(function () {
             $("html,body").css({ "width": "500px", "height": "280px", "background-color": "transparent", "overflow": "hidden" });
             BindString(20, '<%=txtChannelName.ClientID%>');
             BindString(50, '<%=txtChannelGetType.ClientID%>');
             BindInt('<%=txtOrderCode.ClientID%>');
             BindString(100, '<%=txtChannelAddress.ClientID%>');
             BindCtrlEvent('input[check],textarea[check]');
             $("#<%=btnSaveDate.ClientID%>").click(function () {
                 return ValidateForm('input[check],textarea[check]');
             });
         });
     </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
     <table style="width:98%;" class="table table-bordered table-striped;">
        <tr>
            <th colspan="2">添加频道</th>
        </tr>
        <tr>
            <td style="width:60px">频道名称</td>
            <td><asp:TextBox ID="txtChannelName" style="margin:0" check="1" Width="90%" tip="限20个字符！" MaxLength="20" runat="server"></asp:TextBox>
                <span style="color:red">*</span>
            </td>
        </tr>
        <tr>
            <td>频道地址</td>
            <td><asp:TextBox ID="txtChannelAddress" style="margin:0" check="1" Width="90%" tip="限100个字符！" runat="server" MaxLength="100"></asp:TextBox>
                <span style="color:red">*</span>
            </td>
        </tr>
        <tr>
            <td>是否菜单</td>
            <td><asp:CheckBox ID="chkismenu" Checked="true" runat="server"/></td>
        </tr>
        <tr>
            <td>模块类名</td>
            <td><asp:TextBox ID="txtChannelGetType" style="margin:0" Width="45%" check="1" tip="限50个字符！" runat="server" MaxLength="50"></asp:TextBox>
                <span style="color:red">*</span>
            </td>
        </tr>
        <tr>
            <td>排序编号</td>
            <td><asp:TextBox ID="txtOrderCode" style="margin:0" check="0" Width="45%" tip="只能为整数，不填默认为 0" CssClass="{required:true,number:true}" runat="server" MaxLength="10"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2"><asp:Button ID="btnSaveDate" runat="server" CssClass="btn btn-success" Text="添加频道" OnClick="btnSaveDate_Click" /></td>
        </tr>
    </table>
</asp:Content>

 