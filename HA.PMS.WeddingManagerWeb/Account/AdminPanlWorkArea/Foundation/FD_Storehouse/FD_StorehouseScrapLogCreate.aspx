<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FD_StorehouseScrapLogCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseScrapLogCreate" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
<%@ Register assembly="HA.PMS.EditoerLibrary" namespace="HA.PMS.EditoerLibrary" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "99%", "height": "320px", "background-color": "transparent","overflow-y":"hidden" });
        });
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=lblProductName.ClientID%>');
            BindDate('<%=txtScrapDate.ClientID%>');
            BindString(50, '<%=txtReason.ClientID%>');
            BindString(10, '<%=txtScrapEmpLoyee.ClientID%>');
            BindUInt('<%=txtScrapSum.ClientID%>');
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>产品名称</td>
                    <td><asp:Label ID="lblProductName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td><span style="color: red">*</span>报损人</td>
                    <td><asp:TextBox Style="margin: 0" ID="txtScrapEmpLoyee" check="1" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span style="color: red">*</span>报损日期</td>
                    <td><asp:TextBox Style="margin: 0" ID="txtScrapDate" check="1" onclick="WdatePicker();" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span style="color: red">*</span>报损数量</td>
                    <td><asp:TextBox Style="margin: 0" ID="txtScrapSum" check="1" tip="只能为正整数！" runat="server" MaxLength="10"></asp:TextBox></td>
                </tr>
                 <tr>
                    <td><span style="color: red">*</span>报损原因</td>
                    <td><asp:TextBox Style="margin: 0;width:95%" ID="txtReason" check="1" tip="限50个字符！" TextMode="MultiLine" runat="server" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" CssClass="btn btn-success" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
