<%@ Page Title="结算表(查看)" StylesheetTheme="Default" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="DispatchingStatement.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.DispatchingStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_Main">
        <div class="uldiv">
            <table class="table table-bordered table-selected">
                <thead>
                    <tr>
                        <th>类型</th>
                        <th>名称</th>
                        <th>新人姓名</th>
                        <th>婚期</th>
                        <th>总金额</th>
                        <th>已支付</th>
                        <th>未支付</th>
                    </tr>
                </thead>
                <asp:Repeater runat="server" ID="repStatement">
                    <ItemTemplate>

                        <tbody>
                            <tr>
                                <td><%#Eval("TypeName") %></td>
                                <td><%#Eval("Name") %></td>
                                <td><%#GetCustomerName(Eval("CustomerID")) %></td>
                                <td><%#GetPartyDate(Eval("CustomerID")) %></td>
                                <td><%#Eval("SumTotal") %></td>
                                <td><%#Eval("PayMent") %></td>
                                <td><%#Eval("NoPayMent") %></td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
