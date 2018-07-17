<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CA_WeddingSceneEvaluationResultConfig.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.CA_WeddingSceneEvaluationResultConfig" %>
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
        function CheckSuccess(ctrl)
        {
            return ValidateForm('#<%=txtNewName.ClientID%>');
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
            <table class="table table-bordered table-striped" style="width:75%">
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
                                <td width="75">
                                    <asp:TextBox ID="txtName" MaxLength="20" Text='<%#Eval("EvaluationName") %>' Width="130" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbtnSave" OnClientClick="return ValidateThis(this);" CommandName="Edit" CommandArgument='<%#Eval("EvaluationId") %>'  CssClass="btn btn-primary btn-mini" runat="server">保存</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>
                  
                </tbody>
                <tfoot>
                    <tr>
                        <td>
                         评价标准：<asp:TextBox ID="txtNewName" check="1" tip="限20个字符！" MaxLength="20" Width="130"  runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSave"  CssClass="btn btn-success" OnClientClick="return CheckSuccess(this);" OnClick="btnSave_Click" runat="server" Text="保存" />
                        </td>
                    </tr>
                </tfoot>
            </table>

        </div>
    </div>
</asp:Content>
