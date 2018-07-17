<%@ Page Title="支付记录" StylesheetTheme="Default" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="PaymentRecord.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysReport.PaymentRecord" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CustomerTitle.ascx" TagPrefix="HA" TagName="CustomerTitle" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMain">
        <div class="divTop">
            <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
        </div>
        <div class="divMiddle">
            <table class="table table-bordered table-selected" style="width: 98%;">
                <thead>
                    <tr>
                        <th><asp:LinkButton runat="server" ID="lbtnSupplier" Text="供应商" OnClick="lbtnSupplier_Click" /></th>
                        <th><asp:LinkButton runat="server" ID="lbtnPayMoney" Text="支付金额" OnClick="lbtnSupplier_Click"  /></th>
                        <th>支付说明</th>
                        <th>记录人</th>
                        <th><asp:LinkButton runat="server" ID="lbtnCreateDate" Text="记录时间" OnClick="lbtnSupplier_Click"  /></th>
                    </tr>
                </thead>
                <asp:Repeater runat="server" ID="RepPaymentRecord">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("SupplierName") %></td>
                            <td><%#Eval("PayMoney") %></td>
                            <td><%#Eval("Content") %></td>
                            <td><%#GetEmployeeName(Eval("CreateEmployee")) %></td>
                            <td><%#Eval("CreateDate") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
