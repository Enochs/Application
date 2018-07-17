<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CarrytaskWeddingFilesList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWeddingFilesList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "background-color": "transparent" });
            if ('<%=Request["OnlyView"]%>') {
                $(".visibleMark").hide();
            }
        });
        function CheckDelete() {
            return confirm("确认要删除");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:97%">
        <thead>
        <tr><th>文件名</th><th>操作</th></tr>
            </thead>
        <tbody>
        <asp:Repeater ID="repfilelist" runat="server" OnItemCommand="repfilelist_ItemCommand">
            <ItemTemplate>
                <tr skey='<%#Eval("WFileid") %>'>
                    <td><a href="<%#Eval("FileAddress") %>"><%#Eval("FileName") %></a></td>
                    <td><asp:LinkButton CssClass="btn btn-primary btn-mini" CommandName="DownLoad" CommandArgument='<%#Eval("WFileid") %>' runat="server" Text="下载"/>
                    <asp:LinkButton CssClass="btn btn-danger btn-mini visibleMark" OnClientClick="return CheckDelete()" CommandName="Delete" CommandArgument='<%#Eval("WFileid") %>' runat="server" Text="删除"/></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
            </tbody>
    </table>
</asp:Content>