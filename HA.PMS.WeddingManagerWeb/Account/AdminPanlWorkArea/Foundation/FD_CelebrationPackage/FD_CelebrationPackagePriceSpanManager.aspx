<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationPackagePriceSpanManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage.FD_CelebrationPackagePriceSpanManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_CelebrationPackagePriceSpanUpdate.aspx?SpanID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 450, 1000, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {

            showPopuWindows($("#createPrice").attr("href"), 450, 230, "a#createPrice");

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <a href="FD_CelebrationPackagePriceSpanCreate.aspx" class="btn btn-primary  btn-mini" id="createPrice">创建价格段</a>


        <div class="widget-box">
           
            <div class="widget-content">
                <table class="table table-bordered table-striped table-select" style="width:500px;">
                    <thead>
                        <tr>
                            <th>价格段</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptSpan" OnItemCommand="rptSpan_ItemCommand" runat="server">
                            <ItemTemplate>
                                <tr skey='<%#Eval("SpanID") %>'>
                                    <td><%#Eval("SpanPrice") %></td>

                                    <td>

                                        <a href="#" onclick='ShowUpdateWindows(<%#Eval("SpanID") %>,this);' class="btn btn-primary  btn-mini">修改</a>
                                        <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lkbtnDelete"
                                            CommandName="Delete" CommandArgument='<%#Eval("SpanID") %>' runat="server">删除</asp:LinkButton>
                                    </td>

                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <cc1:AspNetPagerTool ID="SpanPager" AlwaysShow="true" OnPageChanged="SpanPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
                <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
            </div>
        </div>
    </div>
</asp:Content>
