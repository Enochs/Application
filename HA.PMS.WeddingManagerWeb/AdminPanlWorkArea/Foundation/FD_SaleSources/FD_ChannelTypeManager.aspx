<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FD_ChannelTypeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_ChannelTypeManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc3" %>
<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        $(window).load(function () {
            BindString(20, '<%=txtChannelTypeName.ClientID%>');
            $("input").each(function () {
                var ctrl = $(this);
                ctrl.attr("orival", ctrl.val());
            }).attr("reg", "^.{1,20}$");
            BindCtrlEvent('input[check]');
            
        });
        function validatethis(ctrl)
        {
            var valc = $(ctrl).parent("td").prev("td").children("input");
            if (valc.val() == '') {
                valc.val(valc.attr("orival"));
                return false;
            }
            else {
                return true;
            }
        }
        function checksave(ctrl)
        {
            return ValidateForm('input[check]');
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <br />
    <table style="width:auto" class="table table-bordered table-striped">
        <thead>
            <tr>
                <th>类别名称</th>
                <th>操作</th>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtChannelTypeName" style="margin:0;margin:0;width:256px" check="1" tip="限20个字符！" MaxLength="20" runat="server"></asp:TextBox></td>
                <td><asp:Button ID="btnSaveAdd" runat="server" Text="保存" OnClientClick="return checksave(this);" CssClass="btn btn-success" OnClick="btnSaveAdd_Click" /></td>
            </tr>
            </thead>
        <tbody>
            <asp:Repeater ID="repChanneType" runat="server" OnItemCommand="repChanneType_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><asp:TextBox style="margin:0;margin:0;width:256px" ID="txtName" tip="限20个字符！" MaxLength="20" runat="server" Text='<%#Eval("ChannelTypeName") %>'></asp:TextBox></td>
                        <td><asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("ChannelTypeId") %>' />
                            <asp:LinkButton ID="lnkbtnSaveEdit" OnClientClick="return validatethis(this);" CssClass="btn btn-primary" CommandName="Edit" CommandArgument='<%#Eval("ChannelTypeId") %>' runat="server">保存修改</asp:LinkButton>
                             <asp:LinkButton ID="lnkbtnDelete" OnClientClick="return validatethis(this);" CssClass="btn btn-primary" CommandName="Delete" CommandArgument='<%#Eval("ChannelTypeId") %>' runat="server">删除</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
</asp:Content>