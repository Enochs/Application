<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CreditLogList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Credit.CreditLogList" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="../../Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 6px;
        }

        .auto-style2 {
            height: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table table-bordered table-striped">
        <thead>
            <tr>
                <th style="white-space: nowrap;" colspan="3">
                    <uc1:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />
                </th>

            </tr>
            <tr>
                <th>时间</th>
                <th>操作人</th>
                <th>积分</th>
                <th>原因</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="reppointList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("CreateDate") %></td>
                        <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                        <td><%#Eval("Point") %></td>
                        <td><%#Eval("Node") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
 
    </table>
</asp:Content>
