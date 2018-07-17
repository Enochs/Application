<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_GuardianTypeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian.FD_GuardianTypeManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_GuardianTypeUpdate.aspx?TypeId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 400, 1000, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            showPopuWindows($("#createType").attr("href"), 400, 700, "a#createType");
        }); 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="FD_GuardianTypeCreate.aspx" class="btn btn-primary  btn-mini" id="createType">创建类型</a>

    <div class="widget-box">
        
        <div class="widget-content">
            <table  class="table table-bordered table-striped table-select" style=" width:600px;">
                <thead>
                    <tr>
                        <th>类型名称</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptType" runat="server" OnItemCommand="rptType_ItemCommand">
                        <ItemTemplate>
                            <tr skey='<%#Eval("TypeId") %>'>
                                <td><%#Eval("TypeName") %></td>
                                <td>
                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("TypeId") %>,this);'>修改</a>
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("TypeId") %>' runat="server">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="TypePager" PageSize="10" AlwaysShow="true" OnPageChanged="TypePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
