<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FL_MoneyRateSpanConfig.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.FL_MoneyRateSpanConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script type="text/javascript">

     //$(document).ready(function () {
     //    $(":text").change(function () {
     //        if (!/^[0-9]{0}([0-9]|[.])+$/.test($(this).val())) { alert('只能输入数字或者两位小数数字'); $(this).val(''); }
     //
     //    });
     //});
      $(window).load(function () {
          BindCtrlRegex();
          BindCtrlEvent('input[check],textarea[check]');
      });
      function BindCtrlRegex() {
          BindUInt('<%=txtNewName.ClientID%>:<%=txtNewName2.ClientID%>');
            $("input").each(function () {
                var ctrl = $(this);
                ctrl.attr("orival", ctrl.val());
            }).attr("reg", "^.{1,20}$");
        }
        function CheckSuccess(ctrl) {
            var min = $(<%=txtNewName.ClientID%>).val();
            var max = $(<%=txtNewName2.ClientID%>).val();
            if (max > min) {
                return ValidateForm('input[check],textarea[check]');
            }
            else {
                alert("起始价格不能高于或等于截止价格！"); $(<%=txtNewName2.ClientID%>).focus(); return false;
            }
        }
        function ValidateThis(ctrl) {
            var valc = $(ctrl).parent("td").prev("td").children("input");
            if ($(valc[0]).val() == '' || $(valc[1]).val() == '') {
                $(valc[0]).val($(valc[0]).attr("orival"));
                $(valc[1]).val($(valc[1]).attr("orival"));
                return false;
            }
            else {
                if ($(valc[1]).val() > $(valc[0]).val()) {
                    return true;
                }
                else {
                    alert("起始价格不能高于或等于截止价格！");
                    $(valc[0]).val($(valc[0]).attr("orival"));
                    $(valc[1]).val($(valc[1]).attr("orival"));
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
         
        <div class="widget-content">
            <table class="table table-bordered table-striped" id="tbContent" style="width:500px;">
                <thead>
                    <tr>
                        <th>利润率段</th>

                        <th>操作</th>

                    </tr>
                </thead>
                <tbody >
                    <asp:Repeater ID="rptDegree" OnItemCommand="rptDegree_ItemCommand" runat="server">
                        <ItemTemplate>

                            <tr>
                                <td>
                                    <asp:TextBox ID="txtName" MaxLength="10" Text='<%#GetPirceString(Eval("RateValue"),0) %>' Width="130" runat="server"></asp:TextBox>

                                    至
                                    <asp:TextBox ID="txtName2" MaxLength="10" Text='<%#GetPirceString(Eval("RateValue"),1) %>' Width="130" runat="server"></asp:TextBox>

                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbtnSave" OnClientClick="return ValidateThis(this);" CommandName="Edit" CommandArgument='<%#Eval("RateId") %>' runat="server" CssClass="btn btn-primary btn-mini">保存</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>

                </tbody>
                <tfoot>
                    <tr>
                        <td>利润率：<asp:TextBox ID="txtNewName" check="1" tip="价格上限百分率！" Width="130" runat="server" MaxLength="10"></asp:TextBox>
                            至<asp:TextBox ID="txtNewName2" check="1" tip="价格下限百分率！" Width="130" runat="server" MaxLength="10"></asp:TextBox>
                            <span style="color:red">*</span>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" OnClick="btnSave_Click" OnClientClick="return CheckSuccess(this);"  runat="server" Text="保存" CssClass="btn  btn-success" />
                        </td>

                    </tr>
                </tfoot>
            </table>

        </div>
    </div>
</asp:Content>
