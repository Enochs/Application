<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="OrderfilDownLoad.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.OrderfilDownLoad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul>
    <asp:Repeater ID="repfileList" runat="server">
        <ItemTemplate>
            <li><a href="<%#Eval("FIleAddress") %>"><%#Eval("Filename") %></a> &nbsp;&nbsp;&nbsp;&nbsp;<a href="<%#Eval("FIleAddress") %>">下载</a> </li>
        </ItemTemplate>
    </asp:Repeater>
        </ul>
</asp:Content>
