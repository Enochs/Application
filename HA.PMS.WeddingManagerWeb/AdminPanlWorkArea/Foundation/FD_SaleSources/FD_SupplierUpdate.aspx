<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_SupplierUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SupplierUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            //$("#ContentPlaceHolder1_txtStarDate").datepicker({ dateFormat: 'yy-mm-dd ' });
        });
        $(window).load(function () {
            BindString(20, '<%=txtName.ClientID%>');
            BindString(5, '<%=txtLinkman.ClientID%>');
            BindDate('<%=txtStarDate.ClientID%>');
            BindMobile('<%=txtCellPhone.ClientID%>');
            BindEmail('<%=txtEmail.ClientID%>');
            BindTel('<%=txtTelPhone.ClientID%>>');
            BindString(30, '<%=txtAddress.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
            $('#<%=txtStarDate.ClientID%>').change(function () { $(this).blur(); });

            if ('<%=Request["OnlyView"]%>') {
                $("input,textarea,select").attr("disabled", "disabled");
                $("input[type=button],.btn").hide();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:PlaceHolder ID="phContent" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>修改供应商</h5>
           
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>供应商名</td>
                    <td>
                        <asp:TextBox ID="txtName" MaxLength="20" check="1" tip="限20个字符！" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                    <td>地址</td>
                    <td>
                        <asp:TextBox ID="txtAddress" MaxLength="30" check="0" CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                    <td>开始合作时间</td>
                    <td>
                        <asp:TextBox ID="txtStarDate" MaxLength="20" check="0" onclick="WdatePicker()" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>联系人</td>
                    <td>
                        <asp:TextBox ID="txtLinkman" MaxLength="10" check="1" CssClass="{required:true}" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                    <td>联系电话</td>
                    <td>
                        <asp:TextBox ID="txtCellPhone" MaxLength="20" check="1" tip="手机号码为11位数字！" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                    <td>QQ</td>
                    <td>
                        <asp:TextBox ID="txtQQ" MaxLength="20" check="0" CssClass="{required:true,number:true,rangelength:[6,15],messages:{number:'请你输入数字',rangelength:'请你输入6到15位的qq号码'}}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>Email</td>
                    <td>
                        <asp:TextBox ID="txtEmail" check="0" CssClass="{required:true,email:true}" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>座机</td>
                    <td>
                        <asp:TextBox ID="txtTelPhone" check="0" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>供应商类别</td>
                    <td>
                        <asp:DropDownList ID="ddlSupplierType" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>开户银行</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtBankName" />
                    </td>
                    <td>银行帐号</td>
                    <td>

                        <asp:TextBox ID="txtAccount" check="0" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="确定" OnClick="btnSave_Click" />
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
        </asp:PlaceHolder>
</asp:Content>
