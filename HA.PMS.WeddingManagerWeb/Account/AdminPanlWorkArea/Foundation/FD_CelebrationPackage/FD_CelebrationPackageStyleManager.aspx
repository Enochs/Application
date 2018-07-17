<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationPackageStyleManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage.FD_CelebrationPackageStyleManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_CelebrationPackageStyleUpdate.aspx?StyleId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 450, 1000, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {

            showPopuWindows($("#createStyle").attr("href"), 450, 700, "a#createStyle");

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <a href="FD_CelebrationPackageStyleCreate.aspx" class="btn btn-primary  " id="createStyle">创建风格</a>


        <div class="widget-box">

            <div class="widget-content">
                <table class="table table-bordered table-striped table-select" style="width:600px;">
                    <thead>
                        <tr>
                            <th>风格ID</th>
                            <th>风格名称</th>
                            <th>风格简介</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptStyle" OnItemCommand="rptStyle_ItemCommand" runat="server">
                            <ItemTemplate>
                                <tr skey='FD_CelebrationPackageStyleId<%#Eval("StyleId") %>'>
                                    <td><%#Eval("StyleId") %></td>
                                    <td><%#Eval("StyleName") %></td>
                                    <td><%#Eval("StyleExplain") %></td>
                                    <td>

                                        <a href="#" onclick='ShowUpdateWindows(<%#Eval("StyleId") %>,this);' class="btn btn-primary  ">修改</a>
                                        <asp:LinkButton CssClass="btn btn-danger " ID="lkbtnDelete"
                                            CommandName="Delete" CommandArgument='<%#Eval("StyleId") %>' runat="server">删除</asp:LinkButton>
                                    </td>

                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <cc1:AspNetPagerTool ID="StylePager" UrlPaging="true" PageSize="10" AlwaysShow="true" OnPageChanged="StylePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
            </div>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
