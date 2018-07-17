<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_TheCaseCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases.FD_TheCaseCreate" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "background-color": "transparent" });
            $("#<%=btnSave.ClientID%>").addClass("disabled").attr("disabled", "disabled");
        });
        $(window).load(function () {
            BindString(20, '<%=txtCaseName.ClientID%>:<%=txtCaseHotel.ClientID%>:<%=txtCaseStyle.ClientID%>:<%=txtCaseGroom.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
        });
        function checksave() {
            //alert(CKEDITOR.instances.txtCaseDetails.getData()); return false;
            return ValidateForm($('input[check],textarea[check]'));
        }
        function uploadpic() {
            $("#<%=flLoad.ClientID%>").click();
        }
        function uploadonchange(ctrl) {
            var _val = $(ctrl).val();
            $('#imgpath').html(_val);
            if (_val == '') {
                $("#<%=btnSave.ClientID%>").addClass("disabled").attr("disabled", "disabled");
            }
            else {
                $("#<%=btnSave.ClientID%>").removeClass("disabled").removeAttr("disabled");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td><span style="color: red">*</span>案例名称</td>
                    <td><asp:TextBox ID="txtCaseName" style="margin:0;width:256px" check="1" tip="限20个字符！" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td><span style="color: red">*</span>酒店</td>
                    <td><asp:TextBox ID="txtCaseHotel" style="margin:0;width:256px" tip="限20个字符！" check="1" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span style="color: red">*</span>新人姓名</td>
                    <td><asp:TextBox ID="txtCaseGroom" style="margin:0;width:256px" tip="限20个字符！" check="1" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td><span style="color: red">*</span>风格</td>
                    <td><asp:TextBox ID="txtCaseStyle" style="margin:0;width:256px" tip="限20个字符！" check="1" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span style="color: red">*</span>封面图片</td>
                    <td colspan="3"><a href="#" class="btn btn-primary btn-mini" title="上传案例封面图片" onclick="uploadpic();return false;">上传图片</a>
                        <span style="font-family:Verdana" id="imgpath"></span>
                        <asp:FileUpload onchange="uploadonchange(this)" style="display:none" ID="flLoad" runat="server" /></td>
                </tr>
                <tr>
                    <td><span style="color: red">*</span>策划手记</td>
                    <td colspan="3"><cc1:CKEditorTool Width="99%" name="txtCaseDetails" ID="txtCaseDetails" runat="server" ClientIDMode="Static"></cc1:CKEditorTool></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="3"><asp:Button ID="btnSave" ToolTip="保存" OnClientClick="return checksave()" OnClick="btnSave_Click" CssClass="btn btn-success" runat="server" Text="保存" /></td>
                </tr>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
