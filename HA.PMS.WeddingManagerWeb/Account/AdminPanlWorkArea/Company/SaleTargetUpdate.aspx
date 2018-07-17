<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="SaleTargetUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.SaleTargetUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/ecmascript">
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindMoney('<%=txtTarget.ClientID%>:<%=txtYear.ClientID%>:<%=txtQuarter.ClientID%>:<%=txtMonth.ClientID%>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>销售目标修改界面</h5>
            <span class="label label-info">修改界面</span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>计划目标</td>
                    <td >
                        <asp:TextBox ID="txtTarget" check="1" CssClass="{required:true,number:true}" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>年度</td>
                    <td>
                        <asp:TextBox ID="txtYear" check="1" CssClass="{required:true,number:true}" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>季度</td>
                    <td>
                        <asp:TextBox ID="txtQuarter" check="1" CssClass="{required:true,number:true}" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>月份</td>
                    <td>
                        <asp:TextBox ID="txtMonth" check="1" CssClass="{required:true,number:true}" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" check="1" runat="server" CssClass="btn btn-success" Text="修改" OnClick="btnSave_Click" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
