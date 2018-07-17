<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_GuradianLevenManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian.FD_GuradianLevenManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_GuradianLevenUpdate.aspx?LevenId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 400, 1000, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {

            showPopuWindows($("#createLeven").attr("href"), 400, 700, "a#createLeven");

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a class="btn btn-primary  btn-mini"  href="FD_GuradianLevenCreate.aspx" id="createLeven">创建等级</a>
    <br />

    <div class="widget-box">
      
        <div class="widget-content">
            <table  class="table table-bordered table-striped table-select" style="width:600px;">
                <thead>
                    <tr>
                        <th>等级名称</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptLeven" OnItemCommand="rptLeven_ItemCommand" runat="server">

                        <ItemTemplate>
                            <tr skey='<%#Eval("LevenId") %>'>
                                <td><%#Eval("LevenName") %></td>
                                <td>
                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("LevenId") %>,this);'>修改</a>
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini"  ID="lkbtn" runat="server" CommandName="Delete" CommandArgument='<%#Eval("LevenId") %>'>删除</asp:LinkButton>

                                </td>
                            </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="LevenPager" PageSize="10" AlwaysShow="true" OnPageChanged="LevenPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
