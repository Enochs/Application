<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationPackageCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage.FD_CelebrationPackageCreate" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(window).load(function () {
            BindString(25, '<%=txtPackageTitle.ClientID%>');
            BindMoney('<%=txtPackagePrice.ClientID%>:<%=txtPackagePreferentiaPrice.ClientID%>');
            BindText(400, '<%=txtPackageDirections.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
        });
        function validate() {
            return ValidateForm('input[check],textarea[check]');
        }
        function uploadpic() {
            $("#<%=flCelePackage.ClientID%>").click();
        }
        function uploadonchange(ctrl) {
            var _val = $(ctrl).val();
            $('#imgpath').html(_val);
            if (_val == '') {
                $("#btnuploadpic").html("选择图片").attr("title", "点击选择套系封面图片");
                $("#<%=btnSave.ClientID%>").addClass("disabled").attr("disabled", "disabled");
            }
            else {
                $("#btnuploadpic").html("已选图片").attr("title", _val);
                $("#<%=btnSave.ClientID%>").removeClass("disabled").removeAttr("disabled");
            }
        }
        $(function () {
            $("#<%=btnSave.ClientID%>").attr("disabled", "disabled");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box" style="width: 99%">
        <div class="widget-content">
            <table style="width: 100%" class="table table-bordered table-striped">
                <tr>
                    <td style="width:64px"><span style="color: red">*</span>套系名称</td>
                    <td style="width:272px"><asp:TextBox ID="txtPackageTitle" Style="margin: 0;width:256px" tip="限25个字符！" check="1" runat="server" MaxLength="25"></asp:TextBox></td>
                    <td style="width:70px">&nbsp;套系风格</td>
                    <td><asp:DropDownList style="margin: 0;width:134px" ID="ddlPackageStyle" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td><span style="color: red">*</span>套系价格</td>
                    <td><asp:TextBox ID="txtPackagePrice" Style="margin: 0" check="1" tip="套系价格" runat="server" MaxLength="10"></asp:TextBox></td>
                    <td style="min-width:80px"><span style="color: red">*</span>优惠价格</td>
                    <td><asp:TextBox ID="txtPackagePreferentiaPrice" Style="margin: 0" check="1" tip="套系优惠价" runat="server" MaxLength="10"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;优惠说明</td>
                    <td colspan="3"><asp:TextBox ID="txtPackageDirections" Style="margin: 0; width: 99%" tip="限400个字符！" check="0" TextMode="MultiLine" runat="server" MaxLength="400"></asp:TextBox></td>
                </tr>
                 <tr>
                    <td>可预订数量</td>
                    <td colspan="3"><asp:TextBox ID="txtSum" Style="margin: 0" check="1" tip="套系优惠价" runat="server" MaxLength="10"></asp:TextBox></td>
                </tr>
                 <tr>
                    <td><span style="color: red">*</span>封面图片</td>
                    <td colspan="3"><a href="#" id="btnuploadpic" class="btn btn-primary btn-mini" title="点击选择套系封面图片" onclick="uploadpic();return false;">上传图片</a>
                        <span style="font-family:Verdana" id="imgpath"></span>
                        <asp:FileUpload onchange="uploadonchange(this)" style="display:none" ID="flCelePackage" runat="server" /></td>
                </tr>
                <tr>
                    <td>&nbsp;套系详情</td>
                    <td colspan="3">
                        <cc1:CKEditorTool ID="txtPackageDetails" Height="200" Width="99%" runat="server" BasePath="~/Scripts/ckeditor/">
                        </cc1:CKEditorTool></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="3"><asp:Button ID="btnSave" CssClass="btn btn-success" OnClientClick="return validate()" runat="server" Text="下一步" OnClick="btnSave_Click" /></td>
                </tr>
                <tr>
                    <td colspan="3"><HA:MessageBoardforall runat="server" ID="MessageBoardforall" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
