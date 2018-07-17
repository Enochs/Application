<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CS_DegreeOfSatisfactionUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_DegreeOfSatisfactionUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 41px;
        }
    </style>
      <script type="text/javascript">
          $(document).ready(function () {
             // $("#<//%=txtDofDate.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd' });

          });
          $(window).load(function () {
              BindCtrlRegex();
              BindCtrlEvent('input[check],textarea[check]');
              $("#<%=txtDofDate.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
          });
        function BindCtrlRegex() {
            BindString(20, '<%=txtDegreeResult.ClientID%>:<%=txtDofContent.ClientID%>:<%=txtSumDof.ClientID%>');
            BindDate('<%=txtDofDate.ClientID%>');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


   <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>客户满意修改界面</h5>
            <span class="label label-info">修改界面</span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
  <tr>
    <td>选择客户</td>
    <td>
        <asp:DropDownList ID="ddlCustomer" runat="server">
                </asp:DropDownList>
    </td>
  </tr>
  <tr>
    <td>调查时间</td>
    <td>
        <asp:TextBox ID="txtDofDate" check="1" onclick="WdatePicker();" runat="server"></asp:TextBox>
        <span style="color:red">*</span>
      </td>
  </tr>
  <tr>

    <td>调查结果</td>
    <td>
        <asp:TextBox ID="txtDegreeResult" check="1" tip="限20个字符！" runat="server"></asp:TextBox>
        <span style="color:red">*</span>
    </td>
  </tr>
  <tr>
    <td>原因建议</td>
    <td>
        <asp:TextBox ID="txtDofContent" check="1" tip="限20个字符！" runat="server"></asp:TextBox>
        <span style="color:red">*</span>
    </td>
  </tr>
  <tr>
    <td>调查满意度</td>
    <td>
        <asp:TextBox ID="txtSumDof" check="1" tip="限20个字符！"  runat="server"></asp:TextBox>
        <span style="color:red">*</span>
    </td>
  </tr>
  <tr>
    <td>调查状态</td>
    <td>
        <asp:DropDownList ID="ddlInvestigateStateID" runat="server"></asp:DropDownList></td>
  </tr>
  <tr>
    <td>
        <asp:Button ID="btnSave" runat="server" Text="确定"  CssClass="btn btn-success"  OnClick="btnSave_Click" /></td>
    <td></td>
  </tr>
</table>
            </div></div>
  </asp:Content>