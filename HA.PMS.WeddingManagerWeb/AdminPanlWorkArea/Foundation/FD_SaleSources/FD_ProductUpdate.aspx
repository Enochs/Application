<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_ProductUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_ProductUpdate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            //$("#txtPutStoreDates").datepicker({ dateFormat: 'yy-mm-dd ' });
            $("html,body").css({ "width": "450px", "height": "650px", "background-color": "transparent" });

        });
        function ChangeSuppByCatogry(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectSupplierBythis.aspx?ALL=true&ControlKey=hideSuppID&SetEmployeeName=txtSuppName&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        $(window).load(function () {
            BindString(20, '<%=txtSourceProductName.ClientID%>:<%=txtSpecifications.ClientID%>:<%=txtUnit.ClientID%>');
            BindMoney('<%=txtPurchasePrice.ClientID%>:<%=txtSaleOrice.ClientID%>');
            BindText(50, '<%=txtExplain.ClientID%>:<%=txtRemark.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnCreate.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;"></a>
    <div class="widget-box">
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>产品名称</td>
                    <td>
                        <asp:TextBox ID="txtSourceProductName" MaxLength="20" tip="限20个字符！" check="1" runat="server" />
                        <span style="color: red">*</span>
                    </td>
                </tr>

                <tr>
                    <td>类别</td>
                    <td>
                        <cc2:CategoryDropDownList ID="ddlCategory" ParentID="0" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" runat="server"></cc2:CategoryDropDownList>

                    </td>
                </tr>
                <tr>
                    <td>项目</td>
                    <td>
                        <cc2:CategoryDropDownList ID="ddlProject" runat="server"></cc2:CategoryDropDownList>
                    </td>
                </tr>

                <tr style='<%=(Request["IsSupplierShow"]!="1"?"display:none": "")%>'>
                    <td>供应商</td>
                    <td id="sup">
                        <input runat="server" maxlength="10" id="txtSuppName" type="text" onclick="ChangeSuppByCatogry(this)" class="txtSuppName " />
                        <asp:HiddenField ID="hideSuppID" Value="-1" runat="server" ClientIDMode="Static" />
                    </td>

                </tr>

                <tr>
                    <td>尺寸</td>
                    <td>
                        <asp:TextBox ID="txtSpecifications" check="0" MaxLength="20" tip="限20个字符！" CssClass="{rangelength:[1,50],messages:{rangelength:'只能是1到50个字内'}}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>资料</td>
                    <td>
                        <asp:FileUpload Width="150" ID="flieUp" ClientIDMode="Static" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>采购价</td>
                    <td>
                        <asp:TextBox ID="txtPurchasePrice" check="1" CssClass="{required:true,number:true}" runat="server"></asp:TextBox><span style="color: red">*</span>

                    </td>
                </tr>
                <tr>
                    <td>销售价</td>
                    <td>
                        <asp:TextBox ID="txtSaleOrice" check="1" CssClass="{number:true}" runat="server"></asp:TextBox><span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>单位</td>
                    <td>
                        <asp:TextBox ID="txtUnit" check="1" tip="产品计量如：个、套" runat="server"></asp:TextBox><span style="color: red">*</span>
                    </td>
                </tr>

                <tr>
                    <td>说明</td>
                    <td>
                        <asp:TextBox ID="txtExplain" check="0" tip="限50个字符！" MaxLength="50" CssClass="{rangelength:[1,50],messages:{rangelength:'只能是1到50个字内'}}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>备注</td>
                    <td>
                        <asp:TextBox ID="txtRemark" check="0" tip="限50个字符！" CssClass="{rangelength:[1,50],messages:{rangelength:'只能是1到50个字内'}}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Button ID="btnCreate" CssClass="btn btn-success" runat="server" Text="保存" OnClick="btnSave_Click" />
                        <%--<cc2:ClickOnceButton ID="btnSave" CssClass="btn btn-success" runat="server" Text="保存" OnClick="btnSave_Click" />--%>

                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>
