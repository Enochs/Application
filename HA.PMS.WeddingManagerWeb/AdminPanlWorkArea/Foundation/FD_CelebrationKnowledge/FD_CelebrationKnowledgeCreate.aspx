<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationKnowledgeCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationKnowledge.FD_CelebrationKnowledgeCreate" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">


        $(document).ready(function () {
            $("#<%=chkTop.ClientID%>").click(function () {
                if ($(this).checked) {
                    $("#<%=ddlParent.ClientID%>").hide();
                } else {
                    $("#<%=ddlParent.ClientID%>").show();
                }
            });
            $("html,body").css({ "width": "1000px", "height": "900px", "background-color": "transparent" });
        });
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtKnowledgeTitle.ClientID%>');
            BindText(65535, '<%=txtKnowContent.ClientID%>');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box" style="width:88%">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>知识库</h5>
        </div>       
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>栏目名称</td>
                    <td><asp:TextBox ID="txtKnowledgeTitle" check="1" tip="限20个字符！" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color:red">*</span></td>
                </tr>
                <tr>
                    <td>父级栏目</td>
                    <td style="white-space:nowrap;"><asp:DropDownList ID="ddlParent" runat="server"></asp:DropDownList>
                        <br />
                        <asp:CheckBox ID="chkTop" Text="" runat="server" />设置该栏目为顶级栏目
                    </td>
                </tr>
                <tr>
                    <td>内容</td>
                    <td>
                        <cc1:CKEditorTool ID="txtKnowContent" check="0" tip="限65535个字符！" runat="server"></cc1:CKEditorTool></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button CssClass="btn btn-primary" ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
