<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_TheCaseUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases.FD_TheCaseUpdate" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "800px", "height": "600px", "background-color": "transparent" });
        });
        $(window).load(function () {
            BindString(20, '<%=txtCaseName.ClientID%>:<%=txtCaseHotel.ClientID%>:<%=txtCaseStyle.ClientID%>:<%=txtCaseGroom.ClientID%>');
            BindUInt('<%=txtCaseOrder.ClientID%>');
            $('#<%=txtCaseOrder.ClientID%>').attr("reg", "^\\d+$");
            BindCtrlEvent('input[check],textarea[check]');
        });
        function checksave() {
            return ValidateForm($('input[check],textarea[check]'));
        }
        function uploadpic() {
            $("#<%=flLoad.ClientID%>").click();
          }
        function uploadonchange(ctrl) {
            var _val = $(ctrl).val();
            $('#imgpath').html(_val);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="widget-box">
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td style="width:80px;"><span style="color:red">*</span>案例名称</td>
                    <td><asp:TextBox ID="txtCaseName" check="1" tip="限20个字符！" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td><span style="color:red">*</span>酒店</td>
                    <td><asp:TextBox ID="txtCaseHotel" tip="限20个字符！" check="1" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span style="color:red">*</span>新人姓名</td>
                    <td><asp:TextBox ID="txtCaseGroom" tip="限20个字符！" check="1" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td><span style="color:red">*</span>风格</td>
                    <td><asp:TextBox ID="txtCaseStyle" tip="限20个字符！" check="1" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                 <tr>
                    <td>封面图片</td>
                    <td colspan="3"><a href="#" class="btn btn-primary btn-mini" title="上传案例封面图片" onclick="uploadpic();return false;">上传图片</a>
                        <span style="font-family:Verdana" id="imgpath"></span>
                        <asp:FileUpload onchange="uploadonchange(this)" style="display:none" ID="flLoad" runat="server" /></td>
                </tr>
                <tr>
                    <td><span style="color:red">*</span>策划手记</td>
                    <td colspan="3"><cc1:CKEditorTool ID="txtCaseDetails" runat="server"></cc1:CKEditorTool></td>
                </tr>
                <tr>
                    <td>序号</td>
                    <td colspan="3"><asp:TextBox runat="server" tip="只能为大于或等于0数字,不填默认为100" Text="100" ID="txtCaseOrder" check="0" MaxLength="8"/></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="3"><asp:Button ID="btnSave" OnClientClick="return checksave()" OnClick="btnSave_Click" CssClass="btn btn-success" runat="server" Text="保存" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
