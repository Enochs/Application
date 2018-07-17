<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FlowerImageList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.FlowerReport.FlowerImageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <table style="width: 97%">
        <tr>
            <th>文件名</th>
            <th>操作</th>
        </tr>
        <asp:Repeater ID="repfilelist" runat="server" OnItemCommand="repfilelist_ItemCommand">
            <ItemTemplate>
                <tr <%#Eval("UploadImage").ToString() == string.Empty ? "style='display:none;'" : "" %>>
                    <td><a target="_blank" href="<%#Eval("UploadImage") %>"><%#Eval("ImageName") %></a></td>
                    <td>
                        <asp:LinkButton ID="lbtnDownLoad" CssClass="btn btn-primary btn-mini" CommandName="DownLoad" CommandArgument='<%#Eval("FlowerKey") %>' runat="server" Text="下载" />
                        <asp:LinkButton ID="lbtnDelete" CssClass="btn btn-danger btn-mini visibleMark" OnClientClick="return CheckDelete()" CommandName="Delete" CommandArgument='<%#Eval("FlowerKey") %>' runat="server" Text="删除" /></td>
                </tr>
                <tr <%#Eval("UploadImage").ToString() == string.Empty ? "style='display:none;'" : "" %>>
                    <td>
                        <a target="_blank" href="<%#Eval("UploadImage") %>">
                            <img src='<%#Eval("UploadImage") %>' alt="" style="width: 200px; height: 150px;" /></a>
                    </td>
                </tr>
                <tr <%#Eval("UploadImage").ToString() == string.Empty ? "style='display:none;'" : "" %>>
                    <td colspan="2">
                        <hr style="border: 1px solid #7a7676; width: 600px;" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
