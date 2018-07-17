<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPricefileDownload.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPricefileDownload" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        $(function () {
            $("html,body").css({ "width": "400px", "height": "400px", "background-color": "transparent" });
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <ul>
        <asp:Repeater ID="repfilelist" runat="server">
            <ItemTemplate>
                <li><a href="<%#Eval("FileAddress") %>"><%#Eval("Filename") %></a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="<%#Eval("FileAddress") %>">下载提案</a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Content>
