<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderMessAge.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.OrderMessAge" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
      <script type="text/javascript">

          $(document).ready(function () {
              $("html,body").css({ "width": "700px", "height": "500px" });
          });

          $(window).load(function () {
              BindString(20, '<%=txtTitle.ClientID%>');
              BindText(65535, '<%=txtContent.ClientID%>');
              BindCtrlEvent('input[check],textarea[check]');
              $("#<%=btnSaveChange.ClientID%>").click(function () {
                  return ValidateForm('input[check],textarea[check]');
              });
          });
    </script>
    <div id="tab1" class="tab-pane active">
        <div class="alert">
            辅导下属
        </div>
        <div class="widget-box">
        </div>
    </div>
    <table class="table table-bordered table-striped with-check">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <asp:Repeater ID="repOrderMessage" runat="server">
            <ItemTemplate>
                <tr>
                    <td>沟通时间：<%#Eval("CreateDate") %><br /><%#Eval("Message") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
              <tr>
            <td>
               标题： <asp:TextBox ID="txtTitle" check="1" tip="限20个字符！" runat="server"></asp:TextBox><span style="color:red">*</span></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtContent" check="1" tip="限65535个字符！"  runat="server" Rows="10" TextMode="MultiLine" Width="100%"></asp:TextBox>
            </td>
        </tr>

  

        <tr>
            <td>
                <asp:Button ID="btnSaveChange" CssClass="btn-success" runat="server" Text="保存" OnClick="btnSaveChange_Click" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>

</asp:Content>


