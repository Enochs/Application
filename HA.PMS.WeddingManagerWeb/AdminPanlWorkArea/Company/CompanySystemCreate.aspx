<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CompanySystemCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.CompanySystemCreate" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">


        $(document).ready(function () {

            $("html,body").css({ "width": "300px", "height": "180px", "background-color": "white" });
        });
        $(window).load(function () {
            BindString(32, '<%=txtSystemTitle.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">


        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td style="white-space: nowrap;">制度名称</td>
                    <td>
                        <asp:TextBox ID="txtSystemTitle" check="1" tip="限32个字符！" runat="server" MaxLength="32"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <%--                <tr>
                    <td style="white-space:nowrap;">父级制度</td>
                    <td style="white-space:nowrap;">
                        <asp:DropDownList ID="ddlParent" runat="server"></asp:DropDownList><br />
                        <asp:CheckBox ID="chkTop" Text="" runat="server" Checked="true" />设置该栏目为顶级制度
                    </td>
                </tr>
                <tr>
                    <td style="white-space:nowrap;">内容</td>
                    <td><cc1:CKEditorTool ID="txtKnowContent" Rows="10"  runat="server"></cc1:CKEditorTool></td>
                  
                </tr>--%>
                <tr>
                    <td>
                        <asp:Button CssClass="btn btn-primary" ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
