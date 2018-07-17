<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CS_ComplainUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_ComplainUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
    <script type="text/javascript">
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtComplainRemark.ClientID%>:<%=txtReturnContent.ClientID%>');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>处理投诉意见界面</h5>
            <span class="label label-info">处理界面</span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>投诉客户</td>
                    <td>
                        <asp:Literal ID="ltlCustomers" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>投诉内容</td>
                    <td>
                      

                        <asp:Literal ID="ltlComplainContent" runat="server"></asp:Literal>

                    </td>
                </tr>
                <tr>
                    <td>投诉时间</td>
                    <td>
                        <asp:Literal ID="ltlComplainDate" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>处理意见</td>
                    <td>
                        <asp:TextBox ID="txtComplainRemark" tip="限20个字符！" check="1" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>处理结果</td>
                    <td>
                        <asp:TextBox ID="txtReturnContent" tip="限20个字符！" check="1" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>


                <tr>
                    <td>
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="确定" OnClick="btnSave_Click" /></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
