<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CA_CompanySaleRateConfig.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.CA_CompanySaleRateConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
    <script type="text/javascript">
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check]');
        });
        function BindCtrlRegex() {
            BindInt('');
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
                //valc.val(parseFloat(valc.val()));
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table class="table table-bordered table-striped" style="width:500px">
                <thead>
                    <tr>
                        <th>质量名称</th>
                        <th>质量值</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptRate" OnItemDataBound="rptRate_ItemDataBound" OnItemCommand="rptRate_ItemCommand" runat="server">
                        <ItemTemplate>


                       
                            <tr>
                                <td>
                                    <asp:Label ID="lblRateName"  runat="server" Text='<%#Eval("Goal") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRateValue" MaxLength="10" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                   <asp:LinkButton CommandName="Oper" OnClientClick="return ValidateThis(this);" CssClass="btn btn-info btn-mini"   CommandArgument='<%#Eval("TargetTypeId") %>' ID="lkbtnOper" runat="server">确认</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>

</asp:Content>
