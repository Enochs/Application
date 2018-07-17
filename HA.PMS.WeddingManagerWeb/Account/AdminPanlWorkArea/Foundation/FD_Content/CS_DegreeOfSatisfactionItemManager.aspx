<%@ Page Title="调查项目维护" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CS_DegreeOfSatisfactionItemManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.CS_DegreeOfSatisfactionItemManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check]');
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtTitle.ClientID%>');
            $("input").each(function () {
                var ctrl = $(this);
                ctrl.attr("orival", ctrl.val());
            }).attr("reg", "^.{1,20}$");
        }
        function CheckSuccess(ctrl) {
            return ValidateForm('#<%=txtTitle.ClientID%>');
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
            <table>
                <tr>
                    <td style="vertical-align:top;">
                        <table class="table table-bordered table-striped" style="width: 500px;">
                            <thead>
                                <tr>
                                    <th>调查项目</th>
                                    <th>操作</th>

                                </tr>
                            </thead>
                            <tbody style="vertical-align: top;">
                                <asp:Repeater ID="repItem" OnItemCommand="repItem_ItemCommand" runat="server">
                                    <ItemTemplate>

                                        <tr skey='<%#Eval("ItemKey") %>'>
                                            <td>
                                                <asp:TextBox ID="txtName" MaxLength="20" Text='<%#Eval("Title") %>' Width="130" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lkbtnDel" CssClass="btn btn-primary btn-mini" CommandName="Delete" CommandArgument='<%#Eval("ItemKey") %>' runat="server">删除</asp:LinkButton>
                                                <asp:LinkButton ID="lkbtnSave" OnClientClick="return ValidateThis(this);" CssClass="btn btn-primary btn-mini" CommandName="Edit" CommandArgument='<%#Eval("ItemKey") %>' runat="server">保存修改</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                </asp:Repeater>

                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtTitle" check="1" tip="限20个字符！" Width="130"  MaxLength="20" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSave" OnClientClick="return CheckSuccess(this);" OnClick="btnSave_Click" runat="server" Text="保存" CssClass="btn  btn-success" />
                                    </td>

                                </tr>
                            </tfoot>
                        </table>
                    </td>
                    <td>

                        <iframe class="framchild " name="main" scrolling="no" noresize id="Iframe1" width="1000" height="600" frameborder="0" name="table" src="/AdminPanlWorkArea/Foundation/FD_Content/CS_DegreeAssessResultConfig.aspx?NeedPopu=1"></iframe>
                    </td>
                </tr>
            </table>

        </div>
    </div>
</asp:Content>
