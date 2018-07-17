<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CS_MemberServiceTypeResultConfig.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.CS_MemberServiceTypeResultConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check]');
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtNewName.ClientID%>');
            $("input").each(function () {
                var ctrl = $(this);
                ctrl.attr("orival", ctrl.val());
            }).attr("reg", "^.{1,20}$");
        }
        function CheckSuccess(ctrl) {
            return ValidateForm('input[check]');
        }
        function ValidateThis(ctrl) {
            var valc = $(ctrl).parent("td").prev("td").children("input");
            if (valc.val() == '') {
                valc.val(valc.attr("orival"));
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div class="widget-box">
      
        <div class="widget-content">
            <table class="table table-bordered table-striped" style="width:500px;">
                <thead>
                    <tr>
                        <th>名称</th>
                        <th>操作</th>

                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDegree" OnItemCommand="rptDegree_ItemCommand" runat="server">
                        <ItemTemplate>

                            <tr>
                                <td>
                                    <asp:TextBox ID="txtName" MaxLength="20" Text='<%#Eval("ServiceTypeName") %>' Width="130" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbtnSave" OnClientClick="return ValidateThis(this);" CommandName="Edit" CommandArgument='<%#Eval("ServiceTypeId") %>' runat="server" CssClass="btn btn-primary btn-mini">保存</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>
                  
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2">服务类型：<br />
                            <asp:TextBox ID="txtNewName" check="1" tip="限20个字符！" Width="130"  runat="server"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <br />

                            </td>
                        <td>
                            <asp:Button ID="btnSave" OnClick="btnSave_Click" OnClientClick="return CheckSuccess(this);" runat="server" Text="保存" CssClass="btn btn-success" />
                        </td>

                    </tr>
                </tfoot>
            </table>

        </div>
    </div>
</asp:Content>