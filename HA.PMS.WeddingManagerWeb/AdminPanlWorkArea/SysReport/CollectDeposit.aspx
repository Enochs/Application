<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CollectDeposit.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysReport.CollectDeposit" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            alert("我只是想测试一下，肯定成功");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-Main">
        <div class="ui-menu-divider">
            <table>
                <tr>
                    <td>姓名:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCustomerName" /></td>
                </tr>
                <tr>
                    <td>婚期：</td>
                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnLookFor" Text="查询" />
                        <cc2:btnReload runat="server" ID="btnReload" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-selected">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th>婚期</th>
                        <th>供应商</th>
                        <th>类别</th>
                        <th>总金额</th>
                        <th>已付款</th>
                        <th>未付款</th>
                        <th>支付</th>
                        <th>付款说明</th>
                        <th>操作</th>

                    </tr>
                </thead>
                <asp:Repeater runat="server" ID="repCustomer">
                    <ItemTemplate>
                        <tr>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
