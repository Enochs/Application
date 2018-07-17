<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_FourGuardianUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian.FD_FourGuardianUpdate" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../../Scripts/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            //$("#<//%=txtStarTime.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            $("html,body").css({ "width": "650px", "height": "600px", "background-color": "transparent" });
        });

        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
            $('#<%=txtStarTime.ClientID%>').change(function () { $(this).blur(); });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtGuardianName.ClientID%>:<%=ddlGuardianTypeId.ClientID%>');
            BindDate('<%=txtStarTime.ClientID%>');
            BindText(200, '<%=txtExplain.ClientID%>:<%=txtPersonalDetails.ClientID%>:<%=txtAccountInformation.ClientID%>');
            BindMobile('<%=txtCellPhone.ClientID%>');
            BindMoney('<%=txtCooperationPrice.ClientID%>:<%=txtSalePrice.ClientID%>');
            BindEmail('<%=txtEmail.ClientID%>');
        }

        $(document).ready(function () {
            $(function () {
                /*JQuery 限制文本框只能输入数字和小数点*/
                $("#txtSort").keyup(function () {
                    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
                }).bind("paste", function () {  //CTR+V事件处理    
                    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
                }).css("ime-mode", "disabled"); //CSS设置输入法不可用    
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">

        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td><span style="color: red">*</span>姓名</td>
                    <td>
                        <asp:TextBox ID="txtGuardianName" check="1" tip="专业团队人员姓名" MaxLength="10" CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">类型</td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="ddlGuardianTypeId" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>等级</td>
                    <td>
                        <asp:DropDownList ID="ddlGuardianLevenId" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>合作时间</td>
                    <td>
                        <asp:TextBox ID="txtStarTime" onclick="WdatePicker();" check="0" tip="不填默认为今天" MaxLength="20" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>联系电话</td>
                    <td>
                        <asp:TextBox ID="txtCellPhone" tip="11位手机号码" check="1" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>合作价格</td>
                    <td>
                        <asp:TextBox ID="txtCooperationPrice" tip="合作价格" check="1" MaxLength="10" runat="server"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>销售价格</td>
                    <td>
                        <asp:TextBox ID="txtSalePrice" check="1" tip="销售价格" runat="server" MaxLength="10"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>邮箱</td>
                    <td>
                        <asp:TextBox ID="txtEmail" check="0" CssClass="{required:true,email:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>人员头像</td>
                    <td>
                        <asp:FileUpload ID="flieUp" runat="server" />
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>账户信息</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtAccountInformation" Width="300" MaxLength="200" /></td>
                </tr>
                <tr>
                    <td>人员简介</td>
                    <td>
                        <%--<asp:TextBox ID="txtExplain" tip="限200个字符！" check="0" MaxLength="200" TextMode="MultiLine" Rows="2" Width="300" CssClass="{required:true}" runat="server"></asp:TextBox>--%>
                        <cc1:CKEditorTool ID="txtExplain" Height="200" Width="95%" runat="server" BasePath="~/Scripts/ckeditor/">
                        </cc1:CKEditorTool>
                    </td>
                </tr>

                <tr>
                    <td>人员详情</td>
                    <td>
                        <%--<asp:TextBox ID="txtPersonalDetails" tip="限200个字符！" check="0" MaxLength="200" Width="300" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>--%>
                        <cc1:CKEditorTool ID="txtPersonalDetails" Height="200" Width="95%" runat="server" BasePath="~/Scripts/ckeditor/">
                        </cc1:CKEditorTool>
                    </td>
                </tr>
                <tr>
                    <td>排序</td>
                    <td><asp:TextBox runat="server" ID="txtSort" ClientIDMode="Static" ToolTip="数值越大,显示就越靠前" /></td>
                </tr>

                <tr>
                    <td>
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="确定" OnClick="btnSave_Click" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
